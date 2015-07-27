using UnityEngine;
using System.Collections;

public class Repeater : Decorator
{
    public Repeater(BehaviorNode parent) : base(parent)
    {

    }


    private int mLoopCount = 0; /**< Number of times to loop the repeater */

    public override IEnumerator Process(BehaviorTree tree)
    {
        for (int i = 0; i < mLoopCount; i++)
        {
            yield return tree.BeginNode(mChild);

            mChild.OnComplete(tree);
        }
            

        mReturnValue = BehaviorReturn.Success;
    }
}
