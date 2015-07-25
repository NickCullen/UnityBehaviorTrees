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

    protected BehaviorReturn mReturnValue = BehaviorReturn.Invalid; /**< as process is ran in coroutines returning a BehaviorReturn type is not allowed. So we store it here and check it with ReturnValue property */

    /**
     * Called once before process
     */
    public virtual void OnStart(BehaviorTree tree) { }

    /**
     * Called once per frame
     */
    public virtual IEnumerator Process(BehaviorTree tree) { yield return null; }

    /**
     * Called once, after process does not return Running
     */
    public virtual void OnComplete(BehaviorTree tree) { }

#region - PROPERTIES -
    public BehaviorReturn ReturnValue
    {
        set { mReturnValue = value; }
        get { return mReturnValue; }
    }
#endregion
}
