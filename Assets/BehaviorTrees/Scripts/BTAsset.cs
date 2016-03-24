using UnityEngine;
using System.Collections;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BTAsset : ScriptableObject 
{
	public string ID;		// ID of this behaviour tree
	public BTNode Root;

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
