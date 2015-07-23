using UnityEngine;
using System.Collections;

public class BehaviorNode 
{
    protected BehaviorReturn mReturnValue = BehaviorReturn.Invalid;

    public virtual IEnumerator Begin(BehaviorTree tree) { yield return null; }


#region - HELPFUL FUNCTIONS -
    public static Coroutine BeginNode(BehaviorTree tree, BehaviorNode node)
    {
        return tree.StartCoroutine(node.Begin(tree));
    }
#endregion

#region - PROPERTIES -
    public BehaviorReturn ReturnValue
    {
        get { return mReturnValue; }
    }
#endregion
}
