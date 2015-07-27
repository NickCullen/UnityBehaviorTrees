using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

/**
 * Gets the item from the BehaviorVariable map via the string id
 * if it is not found it will return Failure.
 * It then attempts to get the stack from the BehaviorVariable map
 * if this is not found it will return Failure
 * If both items are found then the item will be added to the stack
 * and it will return Success
 */
public class PushToStack : Leaf
{
    private string mStack;
    private string mItem;
    
    //cached for efficency
    static object[] input = null;

    /**
     * Ctor
     * @param stack The BehaviorVariable id of stack to push to
     * @param item The BehaviorVariable id item variable
     */
    public PushToStack(BehaviorNode parent, string stack, string item) : base(parent)
    {
        mStack = stack;
        mItem = item;

        //make sure input is instantiated
        if(input == null)
        {
            input = new object[1];
        }
    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        mReturnValue = BehaviorReturn.Failure;
        BehaviorVariable item = tree.GetVariable(mItem);
        if(item != null && item.Variable != null)
        {
            BehaviorVariable stack = tree.GetVariable(mStack);
            if(stack != null && stack.Variable != null)
            {
                MethodInfo methodInfo = stack.Variable.GetType().GetMethod("Push");
                if (methodInfo != null)
                {
                    input[0] = mItem;
                    methodInfo.Invoke(stack.Variable, input);
                    mReturnValue = BehaviorReturn.Success;
                }
            }
        }
        yield return null;
    }
}

