using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class PushToStack : Leaf
{
    private object mStack;
    private object mItem;
    
    //cached for efficency
    object[] input = new object[1];

	/**
     * Ctor
     * @param stack The stack to push to
     * @param item The item variable
     */
    public PushToStack(object stack, object item)
    {
        mStack = stack;
        mItem = item;
    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        mReturnValue = BehaviorReturn.Failure;

        if (mStack != null && mItem != null)
        {
            MethodInfo methodInfo = mStack.GetType().GetMethod("Push");
            if (methodInfo != null)
            {
                input[0] = mItem;
                methodInfo.Invoke(mStack, input);
                mReturnValue = BehaviorReturn.Success;
            }
        }

        yield return null;
    }
}

