using UnityEngine;
using System.Collections;

public class TestLeaf : Leaf
{
    public static int count = 0;
    public int i = 0;

    public string tmp;

    public int number = 0;

    public TestLeaf(BehaviorNode parent, string str) : base(parent)
    {
        tmp = str;
    }

    public override void OnStart(BehaviorTree tree)
    {
        base.OnStart(tree);

        i = ++count;
    }
    public override IEnumerator Process(BehaviorTree tree)
    {

        while (++number != 3)
        {
            Debug.Log(tmp + " = Running");
            mReturnValue = BehaviorReturn.Running;
            yield return null;
        }

        mReturnValue = i > 3 ? BehaviorReturn.Failure : BehaviorReturn.Success;

        yield return null;
    }
}
