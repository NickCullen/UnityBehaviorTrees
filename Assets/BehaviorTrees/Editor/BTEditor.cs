using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BTEditor : EditorWindow
{
	static BTEditor instance;

    BTAsset Asset;

	// Open the editor for the given asset
	public static void OpenWindow(BTAsset asset)
	{
		instance = EditorWindow.GetWindow<BTEditor>(asset.ID);
		instance.Init(asset);
	}


	// Setup links and render nodes
	void Init(BTAsset asset)
	{
        Asset = asset;
		SetupNodes();
        SetupConnectors();
	}

	// Recursive call to create Render Nodes
	void SetupNodes()
	{
        foreach (BTNode node in Asset.Nodes)
        {
            node.Asset = Asset;
        }

	}

    void SetupConnectors()
    {
        foreach (BTNodeConnector con in Asset.Connectors)
        {
            con.Asset = Asset;
        }

    }


    // Render graph
    void OnGUI()
	{

		BeginWindows();

		for( int i = 0; i < Asset.Nodes.Count; i++)
		{
			Asset.Nodes[i].Render(i);
		}

		EndWindows();

        Handles.BeginGUI();

        for (int i = 0; i < Asset.Connectors.Count; i++)
        {
            Asset.Connectors[i].Render(i);
        }

        Handles.EndGUI();
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
