using UnityEngine;

public class TreeCollect : MonoBehaviour
{
    public int woodCount = 0;
    public int seedCount = 0;
    public int appleCount = 0;
    public bool hasGoldApple = false;

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
                    Debug.Log("Chopping main tree");

                    TreeGrow mapTree = hit.collider.GetComponent<TreeGrow>();

                    if (mapTree != null)
                    {
                        GameManager.Instance.activeTrees.Remove(mapTree);
                        Destroy(mapTree.gameObject);
                        SaveSystem.SaveGame();
                    }

                    int woodRand = Random.Range(5, 11);
                    Debug.Log("wood given:" + woodRand);
                    woodCount += woodRand;

                    int seedRand = Random.Range(1, 4);
                    Debug.Log("seed given:" + seedRand);
                    seedCount += seedRand;

                    //appleCount += 2;
                }
                if(hit.collider.CompareTag("AppleTree"))
                {
                    Debug.Log("Chopping apple tree");

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

                    appleCount += 2;
                    Debug.Log("apple given: 2 | apple count: " +appleCount);
                }

                if (hit.collider.CompareTag("GoldenTree"))
                {
                    Debug.Log("Chopping Golden tree");

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

                    appleCount += 1;
                    Debug.Log("apple given: 2 | apple count: " + appleCount);
                    hasGoldApple = true;
                    Debug.Log("Got gold apple");
                }
                //else
                //{
                //    Debug.Log("hit: "+ hit.collider);
                //}

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
