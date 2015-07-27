using UnityEngine;
using System.Collections;

/**
 * Base class all Behavior nodes derive from
 * contains helpful methods to simplify code
 */
public class BehaviorNode
{
    public BehaviorNode(BehaviorNode parent)
    {
        Composite compNode = parent as Composite;
        if (compNode != null)
            compNode.AddChild(this);
        else
        {
            Decorator decNode = parent as Decorator;
            if (decNode != null)
                decNode.AddChild(this);
        }
    }

    public BehaviorReturn mReturnValue = BehaviorReturn.Invalid; /**< as process is ran in coroutines returning a BehaviorReturn type is not allowed. So we store it here and check it with ReturnValue property */

    /**
     * Called once and its a coroutine so you have the OnStart/OnEnd built in!
     */
    public virtual IEnumerator Process(BehaviorTree tree) { yield return null; }

}
