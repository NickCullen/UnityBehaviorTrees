﻿using UnityEngine;
using System.Collections;

public class IsNullObject : Leaf
{
    object mObject;

    /**
     * ctor
     * @param o The object to check if null
     */
	public IsNullObject(BehaviorNode parent, object o) : base (parent)
    {
        mObject = o;
    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        mReturnValue = mObject != null ? BehaviorReturn.Failure : BehaviorReturn.Success;
        yield return null;
    }
}