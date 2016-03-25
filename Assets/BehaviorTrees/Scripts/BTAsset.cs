using UnityEngine;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BTAsset : ScriptableObject 
{
	public string ID;                                                           // ID of this behaviour tree

    public int Root = 0;    // Idx of root node

    public List<BTNode> Nodes = new List<BTNode>();
    public List<BTNodeConnector> Connectors = new List<BTNodeConnector>();




	#if UNITY_EDITOR

	#region - Asset Creation -
	[MenuItem("Assets/Create/New Behaviour Tree")]
	static void CreateAsset()
	{
		BTAsset asset = ScriptableObject.CreateInstance<BTAsset> ();

		string path = AssetDatabase.GetAssetPath (Selection.activeObject);
		if (path == "") 
		{
			path = "Assets";
		} 
		else if (Path.GetExtension (path) != "") 
		{
			path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
		}

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/New " + typeof(BTAsset).ToString() + ".asset");

		AssetDatabase.CreateAsset (asset, assetPathAndName);

		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;

	}
	#endregion


	#endif
}
