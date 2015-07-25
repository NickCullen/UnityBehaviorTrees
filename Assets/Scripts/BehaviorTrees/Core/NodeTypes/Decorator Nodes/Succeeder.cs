using UnityEngine;
using System.Collections;

public class Succeeder : Decorator
{
    public Succeeder(BehaviorNode parent) : base(parent)
    {

    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        yield return tree.BeginNode(mChild);

        mChild.OnComplete(tree);

        //returns success whatever
        mReturnValue = BehaviorReturn.Success;
    }
}
