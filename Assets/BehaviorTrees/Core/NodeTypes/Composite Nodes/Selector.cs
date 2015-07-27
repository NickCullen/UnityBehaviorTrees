using UnityEngine;
using System.Collections;

/**
 * a selector will return a success if any of its children 
 * succeed and not process any further children. It will process 
 * the first child, and if it fails will process the second, 
 * and if that fails will process the third, until a success 
 * is reached, at which point it will instantly return success.
 * It will fail if all children fail. 
 */
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

            if (mChildren[i].mReturnValue == BehaviorReturn.Success)
            {
                //change to success if one of them sucesses and break
                mReturnValue = BehaviorReturn.Success;
                break;
            }
        }
    }
}
