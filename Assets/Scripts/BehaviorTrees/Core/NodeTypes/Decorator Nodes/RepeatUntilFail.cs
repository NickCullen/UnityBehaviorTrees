using UnityEngine;
using System.Collections;

public class RepeatUntilFail : Decorator
{
    public RepeatUntilFail(BehaviorNode parent) : base(parent)
    {

    }


    public override IEnumerator Process(BehaviorTree tree)
    {
        if (mChild != null)
        {
            do
            {
                yield return tree.BeginNode(mChild);

                mChild.OnComplete(tree);
            } while (mChild.ReturnValue != BehaviorReturn.Failure);
        }
        else
            yield return null;
            
        mReturnValue = BehaviorReturn.Success;
    }
}
