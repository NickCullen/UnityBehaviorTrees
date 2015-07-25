using UnityEngine;
using System.Collections;

public class Decorator : BehaviorNode 
{
     /**
     * CTOR
     */
    public Decorator(BehaviorNode parent) : base(parent)
    {
         
    }

    protected BehaviorNode mChild = null;   /**< The only child of a Decorator node */

    /**
    * Called when child node is added when initializing the behaviour node
    */
    public void AddChild(BehaviorNode node)
    {
        mChild = node;
    }
}
