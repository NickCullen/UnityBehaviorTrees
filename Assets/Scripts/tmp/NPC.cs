using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour 
{
    public Transform mPickupSlot1;
    public Transform mPickupSlot2;

    public List<Grid.GridNode> mPath;

    public GameObject mPathObject;
    private List<GameObject> mObjects = new List<GameObject>();
    void Start()
    {
        //mPath = Grid.GetPath(Grid.GetNode(Random.Range(0, Grid.Size)), Grid.GetNode(Random.Range(0, Grid.Size)));
        //foreach (GameObject go in mObjects)
        //    Destroy(go);
        //mObjects.Clear();

        ////show path
        //foreach (Grid.GridNode node in mPath)
        //{
        //    mObjects.Add(Instantiate(mPathObject, node.mPosition, Quaternion.identity) as GameObject);
        //}
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            mPath = Grid.GetPath(Grid.GetNode(Random.Range(0, Grid.Size * Grid.Size)), Grid.GetNode(Random.Range(0, Grid.Size * Grid.Size)));
           // mPath = Grid.GetPath(Grid.GetNode(0), Grid.GetNode((Grid.Size * Grid.Size) -1 ));

            if(mPath.Count == 0)
            {
                Debug.Log("NO PATH FOUND");
            }
            else
            {
                foreach (GameObject go in mObjects)
                    Destroy(go);
                mObjects.Clear();

                //show path
                foreach (Grid.GridNode node in mPath)
                {
                    mObjects.Add(Instantiate(mPathObject, node.mPosition, Quaternion.identity) as GameObject);
                }
            }
            
        }
    }
    public bool PickupItem(Transform item)
    {
        if (mPickupSlot1.childCount == 0)
        {
            item.position = mPickupSlot1.position;
            item.parent = mPickupSlot1;
            return true;
        }
        else if (mPickupSlot2.childCount == 0)
        {
            item.position = mPickupSlot2.position;
            item.parent = mPickupSlot2;
            return true;
        }
        else
            return false;
    }

    public void DropItems()
    {
        for (int i = 0; i < mPickupSlot1.childCount; i++)
            Destroy(mPickupSlot1.GetChild(i).gameObject);
        for (int i = 0; i < mPickupSlot2.childCount; i++)
            Destroy(mPickupSlot2.GetChild(i).gameObject);
    }

    public int AvailableSlots
    {
        get
        {
            int count = 0;
            if (mPickupSlot1.childCount == 0) count++;
            if (mPickupSlot2.childCount == 0) count++;
            return count;
        }
    }
}
