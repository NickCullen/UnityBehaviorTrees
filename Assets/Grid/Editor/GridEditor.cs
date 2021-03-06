﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class GridEditor : EditorWindow 
{
    [MenuItem("Doug Strong/Grid Editor")]
    public static void Init()
    {
        GridEditor window = EditorWindow.GetWindow<GridEditor>();
        window.mGrid = Object.FindObjectOfType<Grid>();
        if (window.mGrid)
        {
            window.mGridSize = window.mGrid.mSize;
        }
    }

    Grid mGrid = null;

    List<GameObject> selectedNodes = null;

    int mGridSize = 0;

    void OnInspectorUpdate()
    {
        if (selectedNodes == null)
            selectedNodes = new List<GameObject>();

        selectedNodes.Clear();
        foreach(GameObject go in Selection.gameObjects)
        {
            if (go.tag == "NavigationNode")
            {
                selectedNodes.Add(go);
            }
        }

        Repaint();
        
    }

    void OnGUI()
    {
        mGrid = Object.FindObjectOfType<Grid>();
        if(mGrid != null)
        {
            mGrid.DisplayGrid = true;

            int size = Mathf.Max(0, EditorGUILayout.IntField("Size", mGridSize));
            //dont allow odd numbers
            mGridSize = size % 2 == 0 ? size : size < mGridSize ? size - 1 : size + 1;

            

            if (GUILayout.Button("Generate Grid"))
            {
                GenerateGrid();
            }

            if(selectedNodes != null && selectedNodes.Count > 0)
            {
                foreach (GameObject go in selectedNodes)
                {
                    GridNode gn = mGrid.ObjectToGridNode(go);
                    if (gn != null) RenderSelectedNodeOptions(gn);
                }
            }               
        }
        else
        {
            Debug.Log("Please create a Grid Object and attach the Grid Component");
        }
        
    }

    //generates grid
    private void GenerateGrid()
    {
        mGrid.mSize = mGridSize;
        mGrid.DisplayGrid = false;
        mGrid.GenerateGrid();   //generating will display objects

    }

    void OnDestroy()
    {
        if (mGrid)
        {
            mGrid.DisplayGrid = false;
        }
    }
    //reners options for selected node
    private void RenderSelectedNodeOptions(GridNode node)
    {
        bool tmp = EditorGUILayout.Toggle("Selected node is walkable", node.mWalkable);
        //they have swapped
        if(tmp != node.mWalkable)
        {
            GameObject go = mGrid.GridNodeToObject(node);
            if(go)
            {
                if(tmp)
                {
                    go.GetComponent<Renderer>().sharedMaterial = mGrid.mWalkableMaterial;
                }
                else
                {
                    go.GetComponent<Renderer>().sharedMaterial = mGrid.mNonWalkableMaterial;
                }
            }
        }

        node.mWalkable = tmp;
    }
}
