using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Grid : MonoBehaviour 
{
    [System.Serializable]
    public class GridNode
    {
        public Vector3 mPosition;

        public bool mWalkable = true;

        //index into Grid.mNodes array
        [HideInInspector()]
        public int mMyIndex = -1;

        //default ctor
        public GridNode() { }

        //actual ctor
        public GridNode(Vector3 vec)
        {
            mPosition = vec;
        }

        public GridNode Left
        {
            get
            {
                int index = mMyIndex - Grid.Width;
                return index < 0 ? null : Grid.Instance.mNodes[index];
            }
        }

        public GridNode Down
        {
            get
            {
                int index = mMyIndex - 1;
                return mMyIndex % Grid.Width == 0 ? null : Grid.Instance.mNodes[index];
            }
        }

        public GridNode Right
        {
            get
            {
                int index = mMyIndex + Grid.Width;
                return index >= Grid.mInstance.mNodes.Count ? null : Grid.Instance.mNodes[index];
            }
        }
        public GridNode Up
        {
            get 
            {
                int index = mMyIndex + 1;
                return index % Grid.Width == 0 ? null : Grid.Instance.mNodes[index];
            }
        }     
    }

    private static Grid mInstance = null;

    //list of grid nodes for navigation
    [HideInInspector()]
    public List<GridNode> mNodes = new List<GridNode>();
    
    //width and height of grid
    public int mSize = 10;

    void Awake()
    {
        mInstance = this;
    }

    #region - PROPERTIES -

    public static Grid Instance
    {
        get
        {
            if (mInstance == null)
                mInstance = Object.FindObjectOfType<Grid>();
            return mInstance;
        }
    }

    public static int Width
    {
        get { return Instance.mSize; }
    }

    public static int Height
    {
        get { return Instance.mSize; }
    }

    public static int Size
    {
        get { return Instance.mSize; }
    }

    #endregion

    #region - EDITOR -
#if UNITY_EDITOR
    //displaying clickable game objects
    private bool mDisplayGrid = false;
    public bool DisplayGrid
    {
        get { return mDisplayGrid; }
        set
        {
            if(mDisplayGrid != value)
            {
                mDisplayGrid = value;
                if (mNodes.Count > 0)
                {
                    if (mDisplayGrid)
                        DisplayObjects();
                    else
                        HideObjects();
                }
            }
        }
    }

    //list of spawned game objects
    List<GameObject> mObjects = new List<GameObject>();

    //parent of mObjects
    GameObject mParent;

    public GameObject mWalkablePrefab;
    public GameObject mNotWalkablePrefab;

    public GridNode mSelectedNode;

    void OnDrawGizmos()
    {
        if(mSelectedNode != null)
        {
            Gizmos.color = Color.red;
            if (mSelectedNode.Left != null) Gizmos.DrawSphere(mSelectedNode.Left.mPosition, 0.25f);
            Gizmos.color = Color.green;
            if (mSelectedNode.Down != null) Gizmos.DrawSphere(mSelectedNode.Down.mPosition, 0.25f);
            Gizmos.color = Color.blue;
            if (mSelectedNode.Right != null) Gizmos.DrawSphere(mSelectedNode.Right.mPosition, 0.25f);
            Gizmos.color = Color.yellow;
            if(mSelectedNode.Up != null) Gizmos.DrawSphere(mSelectedNode.Up.mPosition, 0.25f);
        }
    }

    public void GenerateGrid()
    {
        mNodes.Clear();

        int halfSize = mSize >> 1;

        Vector3 start = new Vector3(-halfSize, 0, -halfSize);
        Vector3 end = new Vector3(halfSize, 0, halfSize);

        float snapX = EditorPrefs.GetFloat("MoveSnapX");
        float snapZ = EditorPrefs.GetFloat("MoveSnapZ");

        float tmpZ = start.z;

        for (; start.x < end.x; start.x += snapX)
        {
            for (start.z = tmpZ; start.z < end.z; start.z += snapZ)
            {
                Grid.GridNode node = new Grid.GridNode(start);
                node.mMyIndex = mNodes.Count;
                mNodes.Add(node);
            }
        }
    }

    public void DisplayObjects()
    {
        //if any are displayed we first need to destroy them!
        HideObjects();

        mParent = new GameObject("Temp Grid Nodes");
        
        foreach(GridNode node in mNodes)
        {
            GameObject go = Instantiate(node.mWalkable ? mWalkablePrefab : mNotWalkablePrefab, node.mPosition, Quaternion.identity) as GameObject;
            go.transform.parent = mParent.transform;
            go.tag = "NavigationNode";
            mObjects.Add(go);
        }
    }
    
    public void HideObjects()
    {
        if (mParent)
            DestroyImmediate(mParent);
        mObjects.Clear();
    }

    public GridNode ObjectToGridNode(GameObject go)
    {
        GridNode ret = null;

        int index = mObjects.IndexOf(go);
        if(index >= 0 && index < mNodes.Count)
            ret = mNodes[index];

        return ret;
    }

    public GameObject GridNodeToObject(GridNode node)
    {
        GameObject ret = null;

        int index = mNodes.IndexOf(node);
        if (index >= 0 && index < mObjects.Count)
            ret = mObjects[index];

        return ret;
    }
#endif
#endregion
}
