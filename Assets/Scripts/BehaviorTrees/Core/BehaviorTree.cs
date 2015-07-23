using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * BehaviorTree component that will attach to a GameObject
 * All MonoBehavior functions must be accessed through this 
 * (.gameObject .transform .GetComponent etc.) These are
 * node cached in BehaviorNode for memory space reasons
 */
public class BehaviorTree : MonoBehaviour 
{
    BehaviorNode mCurrent = null;       /**< The currently executing node */

    Dictionary<string, BehaviorVariable> mVariables = new Dictionary<string,BehaviorVariable>();        /**< Variables for this behavior tree */

    /**
     * Test cases
     */
    void Start()
    {
        Begin(new RandomSelector());
    }

    /**
     * Coroutine to begin the BehaviorTree
     * @param node The root node
     * @return IEnumerator (see Unity Coroutine)
     */
    private IEnumerator Coroutine_Execute(BehaviorNode node)
    {
        mCurrent = node;

        yield return BehaviorNode.BeginNode(this,node);

        mCurrent = null;
    }

    /**
     * Function which starts the BehaviorTree
     * @param node The initial root node
     */
    public void Begin(BehaviorNode node)
    {
        if(node != null)
        {
            StopAllCoroutines();
            StartCoroutine(Coroutine_Execute(node));
        }
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
