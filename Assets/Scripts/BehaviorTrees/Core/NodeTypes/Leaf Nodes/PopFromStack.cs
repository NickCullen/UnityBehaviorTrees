using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

/**
 * Removes object from stack
 */
public class PopFromStack : Leaf 
{

    private string mStack;
    private string mID;

	/**
     * ctor
     * @param stack The stack the item will be popped from
     * @param itemID The id of the item that will be set in the BehaviorTrees variable dictionary when popped
     */
    public PopFromStack(BehaviorNode parent, string stack, string itemID) : base(parent)
    {
        mStack = stack;
        mID = itemID;
    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        mReturnValue = BehaviorReturn.Failure;

        BehaviorVariable stackVar = tree.GetVariable(mStack);
        if (stackVar != null)
        {
            //get required propetys/funcs
            PropertyInfo countProp = stackVar.Variable.GetType().GetProperty("Count");
            MethodInfo methodInfo = stackVar.Variable.GetType().GetMethod("Pop");

            if(countProp != null && methodInfo != null)
            {
                int count = (int)countProp.GetValue(stackVar.Variable,null);
                if(count > 0)
                {
                    object ret = methodInfo.Invoke(stackVar.Variable, null);
                    if (ret != null)
                    {
                        tree.AddVariable(new BehaviorVariable(mID, ret));
                        mReturnValue = BehaviorReturn.Success;
                    }
                }
            }
        }

        yield return null;
    }
}
