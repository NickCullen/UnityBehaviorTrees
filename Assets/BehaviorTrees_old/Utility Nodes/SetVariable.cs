using UnityEngine;
using System.Collections;

/**
 * Takes the string id you want the object to be set to
 * and the object itself.
 * If both are present it will push it onto the trees Behavior Variable 
 * map and return Success. Failure otherwise.
 */
public class SetVariable : Leaf
{
    string mID;
    object mVar;

    /**
     * ctor
     * @param id The variable id
     * @param variable The variable object
     */
    public SetVariable(BehaviorNode parent, string id, object variable) : base(parent)
    {
        mID = id;
        mVar = variable;
    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        mReturnValue = BehaviorReturn.Failure;

        if(mVar != null && !string.IsNullOrEmpty(mID))
        {
            tree.AddVariable(new BehaviorVariable(mID, mVar));
            mReturnValue = BehaviorReturn.Success;
        }

        yield return null;
    }
}
