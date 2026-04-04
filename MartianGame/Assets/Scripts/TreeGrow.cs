using UnityEngine;
using System;

public class TreeGrow : MonoBehaviour
{
    public string id;
    private DateTime endTime;
    private Collider2D col;
    public bool applegiven = false;
    public string Appletag;


    //called when loading game 
    public void Init (TreeSaveData data)
    {
        id = data.id;
        transform.position = data.position;
        endTime = new DateTime(data.endTimeTicks);
        Appletag = gameObject.tag; 
    }
    // when the tree gets planted 
    public void StartNew(Vector3 position)
    {
        id = Guid.NewGuid().ToString();
        transform.position = position;
        endTime = DateTime.UtcNow.AddMinutes(4); //change to hours later
    }

    void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        TimeSpan remaining = endTime - DateTime.UtcNow;
        if (!applegiven && remaining <= TimeSpan.Zero)
        {
            FullyGrown();
            applegiven = true;
        }
    }
    void FullyGrown()
    {
        //Debug.Log("Tree has grown");
        if(gameObject.tag == "Untagged")
        {
            int appleProb = UnityEngine.Random.Range(1, 11);
            Debug.Log(appleProb);
            if (appleProb < 6)
            {
                gameObject.tag = "AppleTree";
                gameObject.transform.localScale = Vector3.one * 0.75f;
                GetComponent<SpriteRenderer>().color = Color.red;
                col.enabled = true;
                return;

            }
            if (appleProb >= 6)
            {
            gameObject.tag = "GoldenTree";
            gameObject.transform.localScale = Vector3.one * 0.75f;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            col.enabled = true;
            return;
            }
            else
            {
            Debug.Log("ruh roh");
            return;
            }

        }
        else
        {
            Debug.Log("trees already tagged");
        }
        
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
            Appletag = gameObject.tag
        };
    }
    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}
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
