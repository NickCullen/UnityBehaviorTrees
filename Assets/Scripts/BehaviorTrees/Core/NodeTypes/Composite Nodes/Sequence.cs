using UnityEngine;
using System.Collections;

public class Sequence : Composite
{
    public Sequence(BehaviorNode parent) : base(parent)
    {

    }


    public override IEnumerator Process(BehaviorTree tree)
    {
        //start with success
        mReturnValue = BehaviorReturn.Success;

        for (int i = 0; i < mChildren.Count; i++ )
        {
            yield return tree.BeginNode(mChildren[i]);

            //remember to call OnComplete
            mChildren[i].OnComplete(tree);

            if (mChildren[i].ReturnValue == BehaviorReturn.Failure)
            {
                //change to failer if one of them fail and break
                mReturnValue = BehaviorReturn.Failure;
                break;
            }
        }
    }
}
