using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BTEditor : EditorWindow
{
	static BTEditor instance;

	List<BTRenderNode> RenderableNodes = new List<BTRenderNode>();

	// Open the editor for the given asset
	public static void OpenWindow(BTAsset asset)
	{
		instance = EditorWindow.GetWindow<BTEditor>(asset.ID);
		instance.Init(asset);
	}


	// Setup links and render nodes
	void Init(BTAsset asset)
	{
		SetupNode(asset.Root);
	}

	// Recursive call to create Render Nodes
	void SetupNode(BTNode node)
	{
		if(node == null) return;

		BTRenderNode rNode = new BTRenderNode();
		rNode.Node = node;

		RenderableNodes.Add(rNode);

		foreach(BTNode child in node.Children)
		{
			SetupNode(child);
		}
	}


	// Render graph
	void OnGUI()
	{

		BeginWindows();

		for( int i = 0; i < RenderableNodes.Count; i++)
		{
			RenderableNodes[i].Render(i);
		}

		EndWindows();
	}



	#region - Asset Open -
	[UnityEditor.Callbacks.OnOpenAsset()]
	public static bool OpenAsset(int instanceID, int line)
	{
		BTAsset bt = EditorUtility.InstanceIDToObject(instanceID) as BTAsset;
		if(bt)
		{
			OpenWindow(bt);
			return true;
		}
		return false; // we did not handle the open
	}

	#endregion
}
