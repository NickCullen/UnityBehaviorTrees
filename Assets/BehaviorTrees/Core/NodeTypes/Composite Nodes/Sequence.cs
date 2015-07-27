using UnityEngine;
using System.Collections;

/**
 * A sequence will visit each child in order, starting with the 
 * first, and when that succeeds will call the second, and 
 * so on down the list of children. If any child fails it will 
 * immediately return failure to the parent. If the last child in 
 * the sequence succeeds, then the sequence will return success to 
 * its parent. 
 */
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

            if (mChildren[i].mReturnValue == BehaviorReturn.Failure)
            {
                //change to failer if one of them fail and break
                mReturnValue = BehaviorReturn.Failure;
                break;
            }
        }
    }
}
