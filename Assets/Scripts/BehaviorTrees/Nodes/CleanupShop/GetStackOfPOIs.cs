using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetStackOfPOIs : Leaf
{
    private string mStackName = "";

    public GetStackOfPOIs(BehaviorNode parent, string stackName) : base(parent)
    {
        mStackName = stackName;
    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        GameObject[] poiObjects = GameObject.FindGameObjectsWithTag("POI");
        if (poiObjects != null && poiObjects.Length > 0)
        {
           
            //the stack
            Stack<GameObject> pois = new Stack<GameObject>();

            //sort by distance
            System.Array.Sort<GameObject>(poiObjects, delegate(GameObject c2, GameObject c1)
            {
                return Vector3.Distance(tree.transform.position, c1.transform.position).CompareTo
                           ((Vector3.Distance(tree.transform.position, c2.transform.position)));
            });

            foreach (GameObject go in poiObjects)
                pois.Push(go);

            tree.AddVariable(new BehaviorVariable(mStackName, pois));

            mReturnValue = BehaviorReturn.Success;
        }
        else
            mReturnValue = BehaviorReturn.Failure;

        yield return null;
    }
}
