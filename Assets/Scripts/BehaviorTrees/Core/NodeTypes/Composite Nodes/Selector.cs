using UnityEngine;
using System.Collections;

public class Selector : Composite
{
    public override IEnumerator Begin(BehaviorTree tree)
    {
        //start with fail this time
        mReturnValue = BehaviorReturn.Failure;

        for (int i = 0; i < mChildren.Count; i++)
        {
            yield return BeginNode(tree, mChildren[i]);

            if (mChildren[i].ReturnValue == BehaviorReturn.Success)
            {
                //change to success if one of them sucesses and break
                mReturnValue = BehaviorReturn.Success;
                break;
            }
        }
    }
}
