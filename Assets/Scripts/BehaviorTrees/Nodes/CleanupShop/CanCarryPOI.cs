using UnityEngine;
using System.Collections;

public class CanCarryPOI : Leaf
{
    public CanCarryPOI(BehaviorNode parent)
        : base(parent)
    {

    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        mReturnValue = BehaviorReturn.Failure;

        NPC npc = tree.GetComponent<NPC>();

        if(npc)
        {
            if (npc.AvailableSlots > 0)
                mReturnValue = BehaviorReturn.Success;
        }

        yield return null;
    }
}
