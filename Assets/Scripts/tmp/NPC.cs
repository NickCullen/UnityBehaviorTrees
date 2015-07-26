using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour 
{
    public float mHopSpeed = 0.5f;
    public float mHopeHeight = 1.0f;

    public AnimationCurve mHopCurve;

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

    Vector3 Interpolate(Vector3 to, Vector3 from, float t)
    {
        return (1.0f - t) * from + (to * t);
    }

    public IEnumerator HopTo(Vector3 pos)
    {
        AnimationCurve curve = AnimationCurve.EaseInOut(Time.time, 0.0f, Time.time + mHopSpeed, 1.0f);
        
        Vector3 startPos = transform.position;
        pos.y = startPos.y;
        Vector3 offset = Vector3.up;
        float timer = 0.0f;
        bool moving = true;

        while(moving)
        {
            timer += Time.deltaTime;
            float time = timer / mHopSpeed;
            transform.position = Vector3.Lerp(startPos, pos + (offset * mHopCurve.Evaluate(time)), time);
            yield return new WaitForEndOfFrame();
            if (transform.position == pos)
            {
                moving = false;
            }
        }
        

        //float t = 0.0f;
        //while (t < 1.0f)
        //{
        //    //t = curve.Evaluate(Time.time);
        //    //current = Interpolate(pos,startPos,t);
        //    //current.y = startY + mHopCurve.Evaluate(Time.time);
        //    t = mHopCurve.Evaluate(Time.time);
        //    transform.position = Vector3.Lerp(startPos, pos + (Vector3.up * t), Time.time);
        //    yield return null;
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
