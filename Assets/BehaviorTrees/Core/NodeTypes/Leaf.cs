using UnityEngine;
using System.Collections;

/** 
 * These are the lowest level node type, and are incapable of 
 * having any children.
 *
 * Leafs are however the most powerful of node types, as these will 
 * be defined and implemented by your game to do the game specific
 * or character specific tests or actions required to make your 
 * tree actually do useful stuff.
 *
 * An example of this, as used above, would be Walk. A Walk leaf 
 * node would make a character walk to a specific point on the 
 * map, and return success or failure depending on the result. 
 */
public class Leaf : BehaviorNode
{
     /**
     * CTOR
     */
    public Leaf(BehaviorNode parent) : base(parent)
    {
         
    }
}
