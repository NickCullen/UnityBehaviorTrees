using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Composite : BehaviorNode 
{
    protected List<BehaviorNode> mChildren = new List<BehaviorNode>();  /**< The list of child nodes */
	
}
