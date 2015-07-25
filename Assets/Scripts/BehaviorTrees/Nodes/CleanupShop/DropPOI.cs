using UnityEngine;
using System.Collections;

public class DropPOI : Leaf
{
    public DropPOI(BehaviorNode parent) : base(parent)
    {

    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        mReturnValue = BehaviorReturn.Failure;
        NPC npc = tree.GetComponent<NPC>();
        if(npc)
        {
            npc.DropItems();
            mReturnValue = BehaviorReturn.Success;        
        }

        yield return null;
    }
    	
}
