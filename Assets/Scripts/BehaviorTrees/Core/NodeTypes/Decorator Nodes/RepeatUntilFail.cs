using UnityEngine;
using System.Collections;

public class RepeatUntilFail : Decorator
{
    public override IEnumerator Process(BehaviorTree tree)
    {
        do
        {
            yield return BeginNode(tree, mChild);
        } while (mChild.ReturnValue != BehaviorReturn.Failure);

        mReturnValue = BehaviorReturn.Failure;
    }
}
