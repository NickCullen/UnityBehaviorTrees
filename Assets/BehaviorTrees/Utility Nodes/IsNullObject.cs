using UnityEngine;
using System.Collections;

public class IsNullObject : Leaf
{
    string mObject;

    /**
     * ctor
     * @param o The object variable name in tree mVariables to check if null
     */
	public IsNullObject(BehaviorNode parent, string o) : base (parent)
    {
        mObject = o;
    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        //if it doesnt exist then its null?
        mReturnValue = BehaviorReturn.Success;

        BehaviorVariable var = tree.GetVariable(mObject);
        if(var != null)
            mReturnValue = var.Variable != null ? BehaviorReturn.Failure : BehaviorReturn.Success;
        yield return null;
    }
}
