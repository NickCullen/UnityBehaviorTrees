using UnityEngine;
using System.Collections;

public class SpawnPOIs : Leaf
{
    GameObject poiPrefab;

    public SpawnPOIs(BehaviorNode parent, GameObject prefab) :base(parent)
    {
        poiPrefab = prefab;
    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        mReturnValue = BehaviorReturn.Failure;

        if(poiPrefab)
        {
            Object.Instantiate(poiPrefab);
            mReturnValue = BehaviorReturn.Success;
        }

        yield return null;
    }
}
