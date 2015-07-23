using UnityEngine;
using System.Collections;

public class Succeeder : Decorator
{
    public override IEnumerator Process(BehaviorTree tree)
    {
        yield return BeginNode(tree, mChild);

        //returns success whatever
        mReturnValue = BehaviorReturn.Success;
    }
}
