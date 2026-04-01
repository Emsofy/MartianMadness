using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player")]
    public int playerLevel = 1;
    public Vector3 playerPosition;

   
    [Header("Dialogue")]
    public HashSet<string> completedDialogues = new HashSet<string>();

    [Header("Time (debug view)")]
    public int lastComputedOfflineSeconds;


    [Header("Dirty Flag")]
    public bool unsavedChanges = false; 

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGame();
    }

    private void Update()
    {
       
      
    }

   

    public void SaveGame()
    {
        SaveSystem.SaveGame();
        unsavedChanges = false; //Or MarkClean();
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.LoadGame();
        if (data == null) return;

        playerLevel = data.playerLevel;
        playerPosition = data.playerPosition;

        completedDialogues = new HashSet<string>(data.completedDialogues ?? new List<string>());

        HandleOfflineTime(data.lastLoginTime);

        // Apply data to scene objects (player, dialogue flags)
        ApplyToScene();

        unsavedChanges = false; // loaded state is clean
    }

    // New method
    private void ApplyToScene()
    {
        // Player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            player.transform.position = playerPosition;

        // DialogueManager
        if (DialogueManager.Instance != null)
        {
            foreach (var id in completedDialogues)
            {
                DialogueManager.Instance.MarkSeen(id); // updates scene dialogue objects
            }
        }
    }
    public void UpdatePlayerPosition(Vector3 newPos)
    {
        if (Vector3.Distance(playerPosition, newPos) > 0.000001f)
        {
            playerPosition = newPos;
            unsavedChanges = true;
        }
    }
    public void HandleOfflineTime(long ticks)
    {
        DateTime lastLogin = new DateTime(ticks);
        TimeSpan timeAway = DateTime.UtcNow - lastLogin;

        int seconds = Mathf.Min((int)timeAway.TotalSeconds, 86400);
        lastComputedOfflineSeconds = seconds;

        Debug.Log("Offline seconds: " + seconds);
        unsavedChanges = true; //or MarkDirty();
    }

    public void HandleDebugTime(int seconds)
    {
        DateTime fakePast = DateTime.UtcNow.AddSeconds(-seconds);
        HandleOfflineTime(fakePast.Ticks);
    }

    // Dialogue helpers
    public void MarkDialogue(string id)
    {
        completedDialogues.Add(id);
        unsavedChanges = true; //or MarkDirty();
    }

    public void ResetDialogue(string id)
    {
        completedDialogues.Remove(id);
        unsavedChanges = true; //or MarkDirty();
    }

    public bool HasSeenDialogue(string id) => completedDialogues.Contains(id);
    public void ResetMemory()
    {
        playerPosition = Vector3.zero;
        completedDialogues.Clear();
        lastComputedOfflineSeconds = 0;
        unsavedChanges = true;
        Debug.Log("Memory Reset");
    }
    public void ResetSaveFile()
    {
        SaveSystem.DeleteSave();
        ResetMemory();
        Debug.Log("Disk Save Reset");
    }
    private void MarkDirty()
    {
        unsavedChanges = true;
    }
    private void MarkClean()
    {
        unsavedChanges = false;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyToScene();
    }
}