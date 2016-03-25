using UnityEngine;
using System.Collections.Generic;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class BTNode
{
    [NonSerialized]
    public BTAsset Asset;      // The asset using this node

    


#if UNITY_EDITOR

    Rect window = new Rect(0, 0, 20, 20);
    public int X
    {
        set { window.x = value; }
        get { return (int)window.x; }
    }
    public int Y
    {
        set { window.y = value; }
        get { return (int)window.y; }
    }
    public int Width
    {
        set { window.xMax = value; }
        get { return (int)(window.xMax - window.x); }
    }
    public int Height
    {
        set { window.yMax = value; }
        get { return (int)(window.yMax - window.y); }
    }

    public void Render(int id)
    {
        window = GUI.Window(id, window, DrawWindowNode, "Node");
    }

    public void DrawWindowNode(int id)
    {
        GUI.DragWindow();
        
    }

	#endif

}
