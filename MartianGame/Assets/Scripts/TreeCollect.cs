using UnityEngine;

public class TreeCollect : MonoBehaviour
{
    public int woodCount = 0;
    public int seedCount = 0;

    public float treeDistance = 2f; //distance player is away from tree for raycast to work
    private movementTest moveScript;
    //public GameObject babytreePrefab;
    public float offest = 1.0f; //offset for raycast 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveScript = GetComponent<movementTest>();
    }

    // Update is called once per frame
    void Update()
    {
        ChopTree();
        PlaceTree();
    }

    public void PlaceTree()
    {
        if (Input.GetKeyDown(KeyCode.E) && seedCount >=1) //add && to check if placeable tile (collision compare tag planter plot)
        {
            Vector2 spawnpos = (Vector2)transform.position + (moveScript.lastDirection * offest);
            GameManager.Instance.PlantTree(spawnpos);
            seedCount--;
            Debug.Log("planting seed");
        }
        else if(Input.GetKeyDown(KeyCode.E) && seedCount<=0)
        {
            Debug.Log("couldn't plant seed");
        }
    }

    public void ChopTree()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector2 origin = (Vector2)transform.position + (moveScript.lastDirection * offest);
            Debug.Log("running tree chop");
            RaycastHit2D hit = Physics2D.Raycast(origin, moveScript.lastDirection, treeDistance); //shoots raycast from player's last move direction
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Tree"))
                {
                    Debug.Log("Chopping tree");

                    TreeGrow tree = hit.collider.GetComponent<TreeGrow>();

                    if (tree != null)
                    {
                        GameManager.Instance.activeTrees.Remove(tree);
                        Destroy(tree.gameObject);
                        SaveSystem.SaveGame();
                    }

                    int woodRand = Random.Range(5, 11);
                    Debug.Log("wood given:" + woodRand);
                    woodCount += woodRand;

                    int seedRand = Random.Range(1, 4);
                    Debug.Log("seed given:" + seedRand);
                    seedCount += seedRand;
                }
                else
                {
                    Debug.Log("hit: "+ hit.collider);
                }

            }
            else
            {
                Debug.Log("didn't hit a tree");
            }
                Debug.DrawRay(transform.position, moveScript.lastDirection * treeDistance, Color.red, 0.5f);
        }
    }

  

    //function for tree growth 
    //4 hours for tree to grow
    //progress bar onto of tree to show how long left
    //make debug to complete timer
    //when tree is done give 2 apples 
    // 1/10 chance to get golden apple 
    //if golden apple is on a tree dont spawn any more
}
