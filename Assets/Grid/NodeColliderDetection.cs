using UnityEngine;
using System.Collections;

public class NodeColliderDetection : MonoBehaviour 
{

	void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
    }
}
