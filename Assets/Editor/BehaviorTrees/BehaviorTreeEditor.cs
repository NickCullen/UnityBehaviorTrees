using UnityEngine;
using UnityEditor;
using System.Collections;

public class BehaviorTreeEditor : EditorWindow
{
    #region - OPENING BEHAVIOR FILES -

    // This method opens the editor window. The "Assets/" part adds it to the Project context menu.
    [MenuItem("Behaviors/Open Behavior Tree")]
    static void EditMyType()
    {
        BehaviorTreeEditor.Init(Selection.activeObject as BehaviorTreeData);
    }

    // This method makes sure the menu item above is only active for objects of MyType:
    [MenuItem("Behaviors/Open Behavior Tree", true)]
    static bool CheckTrue()
    {
        return (Selection.activeObject.GetType() == typeof(BehaviorTreeData));
    }

    public static void Init(BehaviorTreeData dataFile)
    {
        BehaviorTreeEditor editor = (BehaviorTreeEditor)EditorWindow.GetWindow(typeof(BehaviorTreeEditor));
        BehaviorTreeEditor.mInstance = editor;
        editor.Load(dataFile);
    }

    #endregion

    //running instance
    public static BehaviorTreeEditor mInstance;

    //the file data to work upon
    public BehaviorTreeData mData;



    public void Load(BehaviorTreeData data)
    {
        mData = data;   

    }

    void OnGUI()
    {

    }
}
