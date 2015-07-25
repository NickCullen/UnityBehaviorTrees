using UnityEngine;
using System.Collections;

public class PickupPOI : Leaf
{
    private string mPOI = "";

	public PickupPOI(BehaviorNode parent, string itemName) : base(parent)
    {
        mPOI = itemName;
    }

    public override IEnumerator Process(BehaviorTree tree)
    {
        mReturnValue = BehaviorReturn.Failure;

        BehaviorVariable poiVar = tree.GetVariable(mPOI);
        GameObject poiObject = poiVar != null ? poiVar.Variable as GameObject : null;
        NPC npc = tree.GetComponent<NPC>();

        if(npc && poiObject)
        {
            if (npc.PickupItem(poiObject.transform))
                mReturnValue = BehaviorReturn.Success;
        }
        

        yield return null;
    }
}
