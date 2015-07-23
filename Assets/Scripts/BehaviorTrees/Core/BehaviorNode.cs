using UnityEngine;
using System.Collections;

/**
 * Base class all Behavior nodes derive from
 * contains helpful methods to simplify code
 */
public class BehaviorNode 
{
    protected BehaviorReturn mReturnValue = BehaviorReturn.Invalid; /**< as process is ran in coroutines returning a BehaviorReturn type is not allowed. So we store it here and check it with ReturnValue property */

    /**
     * Overload to implement the init process of a node. This will be called before
     * process and is normally used for initialization of variables
     * base.Init(tree) is essential - if you decide to inherit from Init
     * @param tree The behavior tree this node belongs to
     */
    public virtual void Init(BehaviorTree tree) 
    {
        mReturnValue = BehaviorReturn.Running;
    }

    /**
     * Overload to run the process method. This is acoroutine and should yield return atleast once
     * Node base.Process() call is needed!
     * @param tree The behavior tree this node belongs to
     * @return IEnumerator (See Unity Coroutines)
     */
    public virtual IEnumerator Process(BehaviorTree tree) { yield return null; }


#region - HELPFUL FUNCTIONS -
    /**
     * Calls the nodes init and process functions.
     * @param tree The Behavior tree 'node' belongs to
     * @param node The node to begin
     * @return Unity Coroutine
     */
    public static Coroutine BeginNode(BehaviorTree tree, BehaviorNode node)
    {
        //init
        node.Init(tree);
        //process
        return tree.StartCoroutine(node.Process(tree));
    }
#endregion

#region - PROPERTIES -
    public BehaviorReturn ReturnValue
    {
        get { return mReturnValue; }
    }
#endregion
}
