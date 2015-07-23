using UnityEngine;
using System.Collections;

public class BehaviorTree : MonoBehaviour 
{
    BehaviorNode mCurrent = null;

    /**
     * Test cases
     */
    void Start()
    {
        Begin(new RandomSelector());
    }

    private IEnumerator Coroutine_Execute(BehaviorNode node)
    {
        mCurrent = node;

        yield return BehaviorNode.BeginNode(this,node);

        mCurrent = null;
    }

    public void Begin(BehaviorNode node)
    {
        if(node != null)
        {
            StopAllCoroutines();
            StartCoroutine(Coroutine_Execute(node));
        }
    }

    public void Stop()
    {
        if (mCurrent != null)
            StopAllCoroutines();
    }
}
