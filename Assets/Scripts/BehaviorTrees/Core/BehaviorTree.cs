using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#pragma warning disable 219

/**
 * BehaviorTree component that will attach to a GameObject
 * All MonoBehavior functions must be accessed through this 
 * (.gameObject .transform .GetComponent etc.) These are
 * node cached in BehaviorNode for memory space reasons
 */
public class BehaviorTree : MonoBehaviour 
{

    protected BehaviorNode mRoot = null;        /**< Root node of the behavior tree */

    protected BehaviorNode mCurrent = null;       /**< The currently executing node */

    protected Dictionary<string, BehaviorVariable> mVariables = new Dictionary<string,BehaviorVariable>();        /**< Variables for this behavior tree */

    public BehaviorReturn mStatus = BehaviorReturn.Invalid;

    public bool mBehaveOnStart = false;


    /**
     * Coroutine to begin the BehaviorTree
     * @param node The root node
     * @return IEnumerator (see Unity Coroutine)
     */
    private IEnumerator Coroutine_Execute(BehaviorNode node)
    {
        //set this to true as the behavior tree is running
        mStatus = BehaviorReturn.Running;

        mCurrent = node;

        yield return BeginNode(mCurrent);

        mCurrent.OnComplete(this);

        //the status of this tree will be whatever the value of the first node was
        mStatus = mCurrent.ReturnValue;

        Debug.Log("BEHAVIOR COMPLETE " + mStatus);
        mCurrent = null;
    }

    /**
     * Function which starts the BehaviorTree via the root node
     */
    public void Begin()
    {
        if(mRoot != null)
        {
            StopAllCoroutines();
            StartCoroutine(Coroutine_Execute(mRoot));
        }
    }

    /**
     * Begins a node and attaches the coroutine to this object
     * If you use this method you MUST call the node.OnComplete method
     * after it yields
     */
    public Coroutine BeginNode(BehaviorNode node)
    {
        node.ReturnValue = BehaviorReturn.Running;

        //init
        node.OnStart(this);

        //process
        return StartCoroutine(node.Process(this));
    }

    /**
     * Simply stops the Behavior Tree if executing
     */
    public void Stop()
    {
        if (mCurrent != null)
            StopAllCoroutines();
    }

    /**
     * Adds a variable to the dictionary of variables
     * @param var The variable to be added
     */
    public void AddVariable(BehaviorVariable var)
    {
        mVariables[var.VarName] = var;
    }

    /**
     * Deletes a variable from the dictionary of variables
     * @param id The variable id in dictionary
     */
    public void DeleteVariable(string id)
    {
        mVariables.Remove(id);
    }

    /**
     * Returns the variable from dictionary
     * @param id The id of the variable
     * @return Variable node (may be null if not found)
     */
    public BehaviorVariable GetVariable(string id)
    {
        return mVariables.ContainsKey(id) ? mVariables[id] : null;
    }
}
