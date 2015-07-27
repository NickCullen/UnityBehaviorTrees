using UnityEngine;
using System.Collections;
using System.Linq;

/**
 * Same function as normal Sequence node except the order of
 * processing is randomised
 */
public class RandomSequence : Composite
{
    public RandomSequence(BehaviorNode parent) : base(parent)
    {

    }

    private int[] UniqueRandomRange(int lower, int upper)
    {
        System.Random rastgele = new System.Random();
        return Enumerable.Range(lower, upper) // generate sequence
                                  .OrderBy(i => rastgele.Next()) // shuffle
                                  .Take(upper) // if you need only 6 numbers
                                  .ToArray(); // convert to array
    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        int[] indices = UniqueRandomRange(0, mChildren.Count);

        //start with success this time
        mReturnValue = BehaviorReturn.Success;

        foreach(int i in indices)
        {
            yield return tree.BeginNode(mChildren[i]);

            if (mChildren[i].mReturnValue == BehaviorReturn.Failure)
            {
                //change to failer if one of them fail and break
                mReturnValue = BehaviorReturn.Failure;
                break;
            }
        }

    }
}
