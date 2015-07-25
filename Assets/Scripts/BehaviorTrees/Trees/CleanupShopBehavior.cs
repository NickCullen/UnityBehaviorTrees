using UnityEngine;
using System.Collections;

public class CleanupShopBehavior : BehaviorTree
{
    public GameObject mPOIPrefab;

	// Use this for initialization
	void Start () 
    {
        RepeatUntilFail rootRepeat = new RepeatUntilFail(null);

        //root
        Sequence root = new Sequence(rootRepeat);

        //level 1
        GetStackOfPOIs l1_getStack = new GetStackOfPOIs(root, "poiStack");
        RepeatUntilFail l1_RepeatUntilFail = new RepeatUntilFail(root);
        SpawnPOIs l1_spawnPOIs = new SpawnPOIs(root, mPOIPrefab);

        //level 2
        Sequence l2_sequence = new Sequence(l1_RepeatUntilFail);

        //level 3
        Inverter l3_inverter = new Inverter(l2_sequence);
        RepeatUntilFail l3_ruf = new RepeatUntilFail(l2_sequence);
        WalkToObject l3_walkToObject = new WalkToObject(l2_sequence, "Basket");
        DropPOI l3_dropPOI = new DropPOI(l2_sequence);

        //level 4
        IsStackEmpty l4_isStackEmpty = new IsStackEmpty(l3_inverter, "poiStack");
        Sequence l4_sequence = new Sequence(l3_ruf);

        //level 5
        CanCarryPOI l5_canCarry = new CanCarryPOI(l4_sequence);
        Sequence l5_sequence = new Sequence(l4_sequence);

        //level 6
        PopFromStack l6_popFromStack = new PopFromStack(l5_sequence, "poiStack", "item");
        WalkToPOI l6_walkTo = new WalkToPOI(l5_sequence, "item");
        PickupPOI l6_pickup = new PickupPOI(l5_sequence, "item");

        if(mBehaveOnStart) Begin(rootRepeat);
	}
	
	
}
