using UnityEngine;
using System.Collections;

public class WalkToPOI : Leaf
{
    string mPOIVariable = "";

    public WalkToPOI(BehaviorNode parent, string poiVariable) : base(parent)
    {
        mPOIVariable = poiVariable;
    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        NavMeshAgent agent = tree.GetComponent<NavMeshAgent>();
        BehaviorVariable poiVar = tree.GetVariable(mPOIVariable);
        GameObject go = poiVar != null ? poiVar.Variable as GameObject : null;

        if (agent != null && go != null)
        {
            if (agent.SetDestination(go.transform.position))
            {
                do
                {
                    yield return null;
                } while (agent.hasPath);
                    

                mReturnValue = BehaviorReturn.Success;
            }
            else
                mReturnValue = BehaviorReturn.Failure;
            
        }
        else
            mReturnValue = BehaviorReturn.Failure;
    }
}
