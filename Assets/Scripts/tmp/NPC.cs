using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour 
{
    public Transform mPickupSlot1;
    public Transform mPickupSlot2;

    public bool PickupItem(Transform item)
    {
        if (mPickupSlot1.childCount == 0)
        {
            item.position = mPickupSlot1.position;
            item.parent = mPickupSlot1;
            return true;
        }
        else if (mPickupSlot2.childCount == 0)
        {
            item.position = mPickupSlot2.position;
            item.parent = mPickupSlot2;
            return true;
        }
        else
            return false;
    }

    public void DropItems()
    {
        for (int i = 0; i < mPickupSlot1.childCount; i++)
            Destroy(mPickupSlot1.GetChild(i).gameObject);
        for (int i = 0; i < mPickupSlot2.childCount; i++)
            Destroy(mPickupSlot2.GetChild(i).gameObject);
    }

    public int AvailableSlots
    {
        get
        {
            int count = 0;
            if (mPickupSlot1.childCount == 0) count++;
            if (mPickupSlot2.childCount == 0) count++;
            return count;
        }
    }
}
