using UnityEngine;
using UnityEditor;
using System.Collections;

public class BTRenderNode 
{
	BTNode node;
	public BTNode Node
	{
		get {return node;}
		set 
		{
			node = value;
		}
	}

	Rect window = new Rect(0,0,20,20);
	public int X
	{
		set { window.x = value;}
		get { return (int)window.x;}
	}
	public int Y
	{
		set { window.y = value;}
		get { return (int)window.y;}
	}
	public int Width
	{
		set { window.xMax = value;}
		get { return (int)window.xMax;}
	}
	public int Height
	{
		set { window.yMax = value;}
		get { return (int)window.yMax;}
	}
		
	public void Render(int id)
	{
		window = GUI.Window(id, window, DrawWindowNode, "Node");
	}

	public void DrawWindowNode(int id)
	{
		GUI.DragWindow();

	}
}
