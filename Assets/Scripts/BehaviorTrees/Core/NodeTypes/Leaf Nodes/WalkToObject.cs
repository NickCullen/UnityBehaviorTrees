using UnityEngine;
using System.Collections;

public class WalkToObject : Leaf
{
    private string mObject = "";

    public WalkToObject(BehaviorNode parent, string objectName) : base(parent)
    {
        mObject = objectName;
    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        NavMeshAgent agent = tree.GetComponent<NavMeshAgent>();
        GameObject go = GameObject.Find(mObject);

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
