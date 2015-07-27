using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Composite : BehaviorNode
{
    /**
     * CTOR
     */
    public Composite(BehaviorNode parent) : base(parent)
    {
         
    }

    protected List<BehaviorNode> mChildren = new List<BehaviorNode>();  /**< The list of child nodes */

    /**
    * Called when child node is added when initializing the behaviour node
    */
    public void AddChild(BehaviorNode node)
    {
        mChildren.Add(node);
    }
}
