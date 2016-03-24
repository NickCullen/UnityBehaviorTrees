using UnityEngine;
using System.Collections;

/**
 * A repeater will reprocess its child node each time its child 
 * returns a result. These are often used at the very base of 
 * the tree, to make the tree to run continuously. Repeaters may 
 * optionally run their children a set number of times before 
 * returning to their parent. 
 */
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
        }
            

        mReturnValue = BehaviorReturn.Success;
    }
}
