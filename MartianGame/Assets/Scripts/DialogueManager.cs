using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Dialogue Database")]
    public DialogueDatabaseSO database;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional if you want it persistent
    }

    // Mark a dialogue as seen
    public void MarkSeen(string dialogueID)
    {
        var dialogue = database.GetDialogue(dialogueID);
        if (dialogue != null)
        {
            dialogue.seen = true;
            GameManager.Instance.completedDialogues.Add(dialogueID); // keep persistent save state
            GameManager.Instance.unsavedChanges = true;
        }
    }

    // Reset a dialogue
    public void ResetDialogue(string dialogueID)
    {
        var dialogue = database.GetDialogue(dialogueID);
        if (dialogue != null)
        {
            dialogue.seen = false;
            GameManager.Instance.completedDialogues.Remove(dialogueID);
            GameManager.Instance.unsavedChanges = true;
        }
    }

    // Check if dialogue has been seen
    public bool HasSeen(string dialogueID)
    {
        var dialogue = database.GetDialogue(dialogueID);
        return dialogue != null && dialogue.seen;
    }

    // Reset all dialogues
    public void ResetAll()
    {
        foreach (var d in database.dialogues)
            d.seen = false;

        GameManager.Instance.completedDialogues.Clear();
        GameManager.Instance.unsavedChanges = true;
    }
}