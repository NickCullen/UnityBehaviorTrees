using UnityEngine;
using System.Collections;

public class Selector : Composite
{
    public Selector(BehaviorNode parent) : base(parent)
    {

    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        //start with fail this time
        mReturnValue = BehaviorReturn.Failure;

        for (int i = 0; i < mChildren.Count; i++)
        {
            yield return tree.BeginNode(mChildren[i]);

            //remember to call OnComplete
            mChildren[i].OnComplete(tree);

            if (mChildren[i].ReturnValue == BehaviorReturn.Success)
            {
                //change to success if one of them sucesses and break
                mReturnValue = BehaviorReturn.Success;
                break;
            }
        }
    }
}
