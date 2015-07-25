using UnityEngine;
using System.Collections;

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
