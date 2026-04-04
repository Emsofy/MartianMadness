using UnityEngine;
using System;

public class TreeGrow : MonoBehaviour
{
    public string id;
    private DateTime endTime;

    //called when loading game 
    public void Init (TreeSaveData data)
    {
        id = data.id;
        transform.position = data.position;
        endTime = new DateTime(data.endTimeTicks);
    }
    // when the tree gets planted 
    public void StartNew(Vector3 position)
    {
        id = Guid.NewGuid().ToString();
        transform.position = position;
        endTime = DateTime.UtcNow.AddMinutes(4); //change to hours later
    }
  
    // Update is called once per frame
    void Update()
    {
        TimeSpan remaining = endTime - DateTime.UtcNow;
        if (remaining <= TimeSpan.Zero)
        {
            FullyGrown();
        }
    }
    void FullyGrown()
    {
        Debug.Log("Tree has grown");
        //change sprite, add fruits prob
    }
    //saves the time left and position 
    public TreeSaveData GetSaveData()
    {
        return new TreeSaveData
        {
            id = id,
            position = transform.position,
            endTimeTicks = endTime.Ticks,
        };
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SetRemainingSeconds(int seconds)
    {
        endTime = DateTime.UtcNow.AddSeconds(seconds);
    }

    public int GetRemainingSeconds()
    {
        return (int)(endTime - DateTime.UtcNow).TotalSeconds;
    }

    public void ForceGrow()
    {
        endTime = DateTime.UtcNow;
    }
}
