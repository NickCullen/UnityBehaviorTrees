using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

public class BehaviorTreeData : ScriptableObject
{
    public List<XmlNode> mRoot;

    public BehaviorTreeData()
    {
        

    }

    #region - UNITY EDITOR FUNCS -
#if UNITY_EDITOR
    [MenuItem("Behaviors/Create Data File")]
    public static void CreateAsset()
    {
        BehaviorTreeData asset = ScriptableObject.CreateInstance<BehaviorTreeData>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New BehaviorTree.asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

    
#endif
    #endregion
}
