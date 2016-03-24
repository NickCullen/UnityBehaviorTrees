using UnityEngine;
using System.Collections;

/** 
 * A decorator node, like a composite node, can have a child node.
 * Unlike a composite node, they can specifically only have a 
 * single child. Their function is either to transform the result 
 * they receive from their child node's status, to terminate the 
 * child, or repeat processing of the child, depending on the type 
 * of decorator node. 
 */
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
