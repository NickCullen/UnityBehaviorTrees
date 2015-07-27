using UnityEngine;
using System.Collections;

public class Inverter : Decorator
{
    public Inverter(BehaviorNode parent) : base(parent)
    {

    }


    public override IEnumerator Process(BehaviorTree tree)
    {
        yield return tree.BeginNode(mChild);

        mChild.OnComplete(tree);

        //invert the child node
        mReturnValue = mChild.ReturnValue == BehaviorReturn.Success ? BehaviorReturn.Failure : BehaviorReturn.Success;
    }
}
