using UnityEngine;
using System.Collections;

/**
 * Like a repeater, these decorators will continue to reprocess 
 * their child. That is until the child finally returns a failure, 
 * at which point the repeater will return success to its parent. 
 */
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

            } while (mChild.mReturnValue != BehaviorReturn.Failure);
        }
        else
            yield return null;
            
        mReturnValue = BehaviorReturn.Success;
    }
}
