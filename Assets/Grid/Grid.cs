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
                return index < 0 ? null : Grid.Instance.mNodes[index].mWalkable ? Grid.Instance.mNodes[index] : null;
            }
        }

        public GridNode Down
        {
            get
            {
                int index = mMyIndex - 1;
                return mMyIndex % Grid.Width == 0 ? null : Grid.Instance.mNodes[index].mWalkable ? Grid.Instance.mNodes[index] : null;
            }
        }

        public GridNode Right
        {
            get
            {
                int index = mMyIndex + Grid.Width;
                return index >= Grid.mInstance.mNodes.Count ? null : Grid.Instance.mNodes[index].mWalkable ? Grid.Instance.mNodes[index] : null;
            }
        }
        public GridNode Up
        {
            get 
            {
                int index = mMyIndex + 1;
                return index % Grid.Width == 0 ? null : Grid.Instance.mNodes[index].mWalkable ? Grid.Instance.mNodes[index] : null;
            }
        }     

        public List<GridNode> Neighbours
        {
            get
            {
                List<GridNode> neighbours = new List<GridNode>();
                if (Left != null) neighbours.Add(Left);
                if (Down != null) neighbours.Add(Down);
                if (Right != null) neighbours.Add(Right);
                if (Up != null) neighbours.Add(Up);
                return neighbours;
            }
        }
    }

    private static Grid mInstance = null;

    //list of grid nodes for navigation
    public List<GridNode> mNodes = new List<GridNode>();
    
    //width and height of grid
    public int mSize = 10;

    void Awake()
    {
        mInstance = this;
    }

    public static GridNode GetNode(int index)
    {
        return Instance.mNodes[index];
    }

    public static GridNode GetClosestNode(Vector3 position)
    {
        GridNode ret;

        //get a node
        ret = Instance.mNodes.Find(x => Mathf.Abs(x.mPosition.x - position.x) < 0.1f && Mathf.Abs(x.mPosition.z - position.z) < 0.1f);
      
        if (ret != null && ret.mWalkable == false)
        {
            if (ret.Up != null && ret.Up.mWalkable)
                ret = ret.Up;
            else if (ret.Left != null && ret.Left.mWalkable)
                ret = ret.Left;
            else if (ret.Down != null && ret.Down.mWalkable)
                ret = ret.Down;
            else if (ret.Right != null && ret.Right.mWalkable)
                ret = ret.Right;
            //unreachable node
            else
                ret = null;

        }
        else
            Debug.Log("Could not find GridNode for position " + position);

        return ret;
    }

    //heuristic cost
    float HeuristicCost(GridNode from, GridNode to)
    {
        return Vector3.SqrMagnitude(from.mPosition - to.mPosition);
    }

    GridNode GetLowestFromList(Dictionary<GridNode, float> map)
    {
        float lowest = 100000;
        GridNode ret = null;
        foreach (KeyValuePair<GridNode, float> entry in map)
        {
            if(entry.Value < lowest)
            {
                lowest = entry.Value;
                ret = entry.Key;
            }
        }

        map.Remove(ret);
        return ret;
    }

    public List<GridNode> ReconstructPath(Dictionary<GridNode, GridNode> cameFrom, GridNode current)
    {
        List<GridNode> path = new List<GridNode>();
        path.Add(current);

        while(cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }
        path.Reverse();
        return path;
    }

    //A* search
    public static List<GridNode> GetPath(GridNode start, GridNode goal)
    {
        List<GridNode> closedSet = new List<GridNode>(); //list if set of nodes already evaluated
        List<GridNode> openSet = new List<GridNode>();  //the set of tentative nodes to be evaluated, initially
        openSet.Add(start);
        Dictionary<GridNode,GridNode> cameFrom = new Dictionary<GridNode,GridNode>(); //The map of navigated nodes

        //cost from start along best known path
        Dictionary<GridNode, float> g_score = new Dictionary<GridNode, float>();
        g_score[start] = 0.0f;

        //estimated total cost from start to goal through y
        Dictionary<GridNode, float> f_score = new Dictionary<GridNode, float>();
        f_score[start] = g_score[start] + Instance.HeuristicCost(start, goal);

        GridNode current = null;
        while(openSet.Count > 0)
        {
            current = Instance.GetLowestFromList(f_score);
            if (current == goal)
                return Instance.ReconstructPath(cameFrom, goal);

            openSet.Remove(current);
            closedSet.Add(current);

            foreach(GridNode neighbor in current.Neighbours)
            {
                if (closedSet.Contains(neighbor))
                    continue;

                float tentative_g_score = g_score[current] + Vector3.SqrMagnitude(current.mPosition - neighbor.mPosition);
                
                if(!openSet.Contains(neighbor) || tentative_g_score < g_score[neighbor])
                {
                    cameFrom[neighbor] = current;
                    g_score[neighbor] = tentative_g_score;
                    f_score[neighbor] = g_score[neighbor] + Instance.HeuristicCost(neighbor, goal);

                    if(!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return new List<GridNode>();
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
