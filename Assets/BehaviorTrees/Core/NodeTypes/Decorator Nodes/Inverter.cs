using UnityEngine;
using System.Collections;

/**
 * Simply put they will invert or negate the result of their 
 * child node. Success becomes failure, and failure becomes 
 * success. They are most often used in conditional tests. 
 */
public class Inverter : Decorator
{
    public Inverter(BehaviorNode parent) : base(parent)
    {

    }


    public override IEnumerator Process(BehaviorTree tree)
    {
        yield return tree.BeginNode(mChild);

        //invert the child node
        mReturnValue = mChild.mReturnValue == BehaviorReturn.Success ? BehaviorReturn.Failure : BehaviorReturn.Success;
    }
}
