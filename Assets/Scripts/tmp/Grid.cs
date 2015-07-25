using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Grid : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

#region - EDITOR -
#if UNITY_EDITOR
    public bool mDisplayGrid = false;

    public Color mGizmoColor = Color.yellow;

    public float mGizmoRadius = 0.15f;

    void OnDrawGizmos()
    {
        Vector3 start = new Vector3(-15, 0, -15);
        Vector3 end = new Vector3(15, 0, 15);
        Vector3 diff = end - start;

        float snapX = EditorPrefs.GetFloat("MoveSnapX");
        float snapZ = EditorPrefs.GetFloat("MoveSnapZ");

        Gizmos.color = mGizmoColor;

        float tmpZ = start.z;

        for(; start.x < end.x; start.x += snapX)
        {
            for(start.z = tmpZ; start.z < end.z; start.z += snapZ)
            {
                Gizmos.DrawSphere(start, mGizmoRadius);
            }
        }
    }
#endif
#endregion
}
