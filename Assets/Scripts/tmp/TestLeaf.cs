﻿using UnityEngine;
using System.Collections;

public class TestLeaf : Leaf
{
    public override IEnumerator Begin(BehaviorTree tree)
    {
        mReturnValue = Random.Range(0, 2) > 0 ? BehaviorReturn.Failure : BehaviorReturn.Success;

        yield return null;
    }
}
