using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    public int woodCount = 0;
    public int seedCount = 0;

// planting trees
//growth cycle
//probability on reg vs special 
//if special no more special
//chopping tree(e)
//wood count 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceTree()
    {
        if (Input.GetKeyDown(KeyCode.E) && seedCount >=1) //add && to check if placeable tile (collision compare tag planter plot)
        {
            //place seed to grow tree
        }
    }

    public void ChopTree(int woodCount)
    {
        //press key compare if tree
        //destroy tree object
        //give player (5-10(wood))
        //give player seed (0-2)
    }

    //function for tree growth 
    //4 hours for tree to grow
    //progress bar onto of tree to show how long left
    //make debug to complete timer
    //when tree is done give 2 apples 
    // 1/10 chance to get golden apple 
    //if golden apple is on a tree dont spawn any more
}
