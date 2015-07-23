using UnityEngine;
using System.Collections;

public class Inverter : Decorator
{
    public override IEnumerator Process(BehaviorTree tree)
    {
        yield return BeginNode(tree, mChild);

        //invert the child node
        mReturnValue = mChild.ReturnValue == BehaviorReturn.Success ? BehaviorReturn.Failure : BehaviorReturn.Success;
    }
}
