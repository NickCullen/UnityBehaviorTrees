using UnityEngine;
using System.Collections;
using System.Reflection;

/**
 * Checks to see if the stack on the Behaviors variable map is empty
 * will return Success if it is empty or non-existant. will return
 * false otherwise
 */
public class IsStackEmpty : Leaf
{
    string mStack = null;

    /**
     * Constructor
     * @param count The stack count to check is empty or not
     */
    public IsStackEmpty(BehaviorNode parent, string stack) : base(parent)
    {
        mStack = stack;
    }

    
    public override IEnumerator Process(BehaviorTree tree)
    {
        mReturnValue = BehaviorReturn.Success;
        BehaviorVariable stackVar = tree.GetVariable(mStack);

        if(stackVar != null && stackVar.Variable != null)
        {
            PropertyInfo countProperty = stackVar.Variable.GetType().GetProperty("Count");
            if(countProperty != null)
            {
                object val = countProperty.GetValue(stackVar.Variable, null);
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
