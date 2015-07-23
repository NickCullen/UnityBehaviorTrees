using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

/**
 * Removes object from stack
 */
public class PopFromStack : Leaf 
{

    private object mStack;
    private string mID;

	/**
     * ctor
     * @param stack The stack the item will be popped from
     * @param itemID The id of the item that will be set in the BehaviorTrees variable dictionary when popped
     */
    public PopFromStack(object stack, string itemID)
    {
        mStack = stack;
        mID = itemID;
    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        mReturnValue = BehaviorReturn.Failure;

        if(mStack != null && !string.IsNullOrEmpty(mID))
        {
            MethodInfo methodInfo = mStack.GetType().GetMethod("Pop");
            if(methodInfo != null)
            {
                object ret = methodInfo.Invoke(mStack, null);
                if(ret != null)
                {
                    tree.AddVariable(new BehaviorVariable(mID,ret));
                    mReturnValue = BehaviorReturn.Success;
                }
            }
        }

        yield return null;
    }
}
