using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 
 * A composite node is a node that can have one or more children.
 * They will process one or more of these children in either a 
 * first to last sequence or random order depending on the
 * particular composite node in question, and at some stage will 
 * consider their processing complete and pass either success or 
 * failure to their parent, often determined by the success or 
 * failure of the child nodes. During the time they are processing
 * children, they will continue to return Running to the parent. 
 */
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
