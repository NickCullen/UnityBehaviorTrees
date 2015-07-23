using UnityEngine;
using System.Collections;

public class Sequence : Composite
{
    public override IEnumerator Begin(BehaviorTree tree)
    {
        //start with success
        mReturnValue = BehaviorReturn.Success;

        for (int i = 0; i < mChildren.Count; i++ )
        {
            yield return BeginNode(tree,mChildren[i]);

            if (mChildren[i].ReturnValue == BehaviorReturn.Failure)
            {
                //change to failer if one of them fail and break
                mReturnValue = BehaviorReturn.Failure;
                break;
            }
        }
    }
}
