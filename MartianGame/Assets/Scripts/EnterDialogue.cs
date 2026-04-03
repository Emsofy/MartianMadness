using UnityEngine;

public class EnterDialogue : MonoBehaviour
{
 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTrigger2D(Collider2D other)
    {
        if (other.CompareTag("Alien"))
        {
            Debug.Log("triggered");

            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Dialogue start");
            }
        }
    }
}
