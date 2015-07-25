using UnityEngine;
using System.Collections;
using System.Reflection;

/**
 * Returns success or fail depending weither or not mStack is empty or not
 */
public class IsStackEmpty : Leaf
{
    object mStack = null;

    /**
     * Constructor
     * @param count The stack count to check is empty or not
     */
    public IsStackEmpty(BehaviorNode parent, object stack) : base(parent)
    {
        mStack = stack;
    }

    
    public override IEnumerator Process(BehaviorTree tree)
    {
        mReturnValue = BehaviorReturn.Success;

        if(mStack != null)
        {
            PropertyInfo countProperty = mStack.GetType().GetProperty("Count");
            if(countProperty != null)
            {
                object val = countProperty.GetValue(mStack, null);
                if(val != null)
                {
                    int result = (int)val;
                    mReturnValue = result > 0 ? BehaviorReturn.Failure : BehaviorReturn.Success;
                }
            }
        }
        yield return null;
    }
}
