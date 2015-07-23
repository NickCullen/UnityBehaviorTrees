using UnityEngine;
using System.Collections;

public class Decorator : BehaviorNode 
{
    protected BehaviorNode mChild = null;   /**< The only child of a Decorator node */
}
