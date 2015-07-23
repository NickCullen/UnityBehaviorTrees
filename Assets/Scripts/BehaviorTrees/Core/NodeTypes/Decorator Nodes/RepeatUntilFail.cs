using UnityEngine;
using System.Collections;

public class RepeatUntilFail : Decorator
{
    public override IEnumerator Begin(BehaviorTree tree)
    {
        do
        {
            yield return BeginNode(tree, mChild);
        } while (mChild.ReturnValue != BehaviorReturn.Failure);

        mReturnValue = BehaviorReturn.Failure;
    }
}
