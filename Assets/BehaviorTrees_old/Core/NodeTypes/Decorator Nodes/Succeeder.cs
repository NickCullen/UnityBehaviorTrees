using UnityEngine;
using System.Collections;

/**
 * A succeeder will always return success, irrespective of what
 * the child node actually returned. These are useful in cases 
 * where you want to process a branch of a tree where a failure 
 * is expected or anticipated, but you don’t want to abandon 
 * processing of a sequence that branch sits on. The opposite of 
 * this type of node is not required, as an inverter will turn a 
 * succeeder into a ‘failer’ if a failure is required for the 
 * parent. 
 */
public class Succeeder : Decorator
{
    public Succeeder(BehaviorNode parent) : base(parent)
    {

    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        yield return tree.BeginNode(mChild);

        //returns success whatever
        mReturnValue = BehaviorReturn.Success;
    }
}
