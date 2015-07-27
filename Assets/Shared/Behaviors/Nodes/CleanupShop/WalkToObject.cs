using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalkToObject : Leaf
{
    private string mObject = "";
    List<GridNode> mPath;

    public WalkToObject(BehaviorNode parent, string objectName) : base(parent)
    {
        mObject = objectName;
    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        GameObject go = GameObject.Find(mObject);
        NPC npc = tree.GetComponent<NPC>();

        if (go && npc)
        {
            GridNode goal = Grid.GetClosestNode(go.transform.position);
            GridNode myNode = Grid.GetClosestNode(tree.transform.position);

            if (goal != null && myNode != null)
            {
                mPath = Grid.GetPath(myNode, goal);
                if (mPath.Count > 0)
                {
                    for (int i = 0; i < mPath.Count; i++)
                    {
                        yield return tree.StartCoroutine(npc.HopTo(mPath[i].mPosition));
                    }

                    mReturnValue = BehaviorReturn.Success;
                }
                else
                    mReturnValue = BehaviorReturn.Failure;

            }
            else
                mReturnValue = BehaviorReturn.Failure;
        }
        else
            mReturnValue = BehaviorReturn.Failure;
    }
}
