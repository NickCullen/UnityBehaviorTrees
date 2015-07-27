using UnityEngine;
using System.Collections;
using System.Linq;

public class RandomSelector : Composite
{
    public RandomSelector(BehaviorNode parent) : base(parent)
    {

    }

    private int [] UniqueRandomRange(int lower, int upper)
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

        //start with fail this time
        mReturnValue = BehaviorReturn.Failure;

        foreach (int i in indices)
        {
            yield return tree.BeginNode(mChildren[i]);

            //remember to call OnComplete
            mChildren[i].OnComplete(tree);

            if (mChildren[i].ReturnValue == BehaviorReturn.Success)
            {
                //change to success if one of them sucesses and break
                mReturnValue = BehaviorReturn.Success;
                break;
            }
        }

    }
	
}
