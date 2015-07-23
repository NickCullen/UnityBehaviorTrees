using UnityEngine;
using System.Collections;
using System.Linq;

public class RandomSequence : Composite
{

    private int[] UniqueRandomRange(int lower, int upper)
    {
        System.Random rastgele = new System.Random();
        return Enumerable.Range(lower, upper) // generate sequence
                                  .OrderBy(i => rastgele.Next()) // shuffle
                                  .Take(upper) // if you need only 6 numbers
                                  .ToArray(); // convert to array
    }

    public override IEnumerator Begin(BehaviorTree tree)
    {
        int[] indices = UniqueRandomRange(0, mChildren.Count);

        //start with success this time
        mReturnValue = BehaviorReturn.Success;

        foreach(int i in indices)
        {
            yield return BeginNode(tree, mChildren[i]);

            if (mChildren[i].ReturnValue == BehaviorReturn.Failure)
            {
                //change to failer if one of them fail and break
                mReturnValue = BehaviorReturn.Failure;
                break;
            }
        }

    }
}
