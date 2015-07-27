using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalkToPOI : Leaf
{
    string mPOIVariable = "";
    List<GridNode> mPath;

    public WalkToPOI(BehaviorNode parent, string poiVariable) : base(parent)
    {
        mPOIVariable = poiVariable;
    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        BehaviorVariable poiVar = tree.GetVariable(mPOIVariable);
        GameObject go = poiVar != null ? poiVar.Variable as GameObject : null;
        NPC npc = tree.GetComponent<NPC>();

        if (go && npc)
        {
            GridNode goal = Grid.GetClosestNode(go.transform.position, tree.transform.position);
            GridNode myNode = Grid.GetClosestNode(tree.transform.position);

            if (goal != null && myNode != null)
            {
                mPath = Grid.GetPath(myNode, goal);
                if (mPath.Count > 0)
                {
                    
                    for (int i = 0; i < mPath.Count; i++)
                    {
                        Vector3 pos = mPath[i].mPosition;
                        pos.y = tree.transform.position.y;
                        tree.transform.LookAt(pos);
                        tree.transform.position = pos;
                        yield return new WaitForSeconds(0.5f);
                        //yield return tree.StartCoroutine(npc.HopTo(pos));
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
