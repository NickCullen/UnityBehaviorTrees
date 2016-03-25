using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System;

[Serializable]
public class BTNodeConnector
{
    [NonSerialized]
    public BTAsset Asset;

    public int firstIdx = -1;      // First side of connector

    public int secondIdx = -1;     // Second side of connector

    BTNode First
    {
        get
        {
            if (firstIdx < 0) return null;
            else if (firstIdx >= Asset.Nodes.Count) return null;
            else return Asset.Nodes[firstIdx];
        }
    }

    BTNode Second
    {
        get
        {
            if (secondIdx < 0) return null;
            else if (secondIdx >= Asset.Nodes.Count) return null;
            else return Asset.Nodes[secondIdx];
        }
    }

#if UNITY_EDITOR


    public void Render(int id)
    {
        if (First == null || Second == null) return;

        Vector3 start = new Vector3(First.X + (First.Width * 0.5f), First.Y + First.Width, 0.0f);
        Vector3 end = new Vector3(Second.X + (Second.Width * 0.5f), Second.Y, 0.0f);

        Handles.DrawBezier(start, end, end, start, Color.white, Texture2D.whiteTexture, 1.0f);

        
    }


#endif
}
