using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
#endif

/**
 * Grid Singleton used for path finding amoungst the 
 * grid nodes. To be used with the Grid Editor to generate
 * the list of GridNodes
 */
public class Grid : MonoBehaviour 
{
    //cached rogue value vector
    private static Vector3 ROGUE_VECTOR = new Vector3(-989,989,-989);

    //the current singleton instance. If another is created - the old will be destroyed.
    private static Grid mInstance = null;

    //list of serialized grid nodes for navigation in this grid
    public List<GridNode> mNodes = new List<GridNode>();
    
    //width and height of grid
    public int mSize = 10;

    //snap values of the grid
    private float mSnapX = 1.0f;
    private float mSnapY = 1.0f;
    private float mSnapZ = 1.0f;

    void Awake()
    {
        if (mInstance != null)
            Destroy(mInstance);
        mInstance = this;
    }

    public static GridNode GetNode(int index)
    {
        return Instance.mNodes[index];
    }

    /**
     * Returns the closest node to the given position. An overloaded argument may
     * be given so that it will also return the closest node relative to that.
     */
    public static GridNode GetClosestNode(Vector3 position)
    {
        return GetClosestNode(position, ROGUE_VECTOR);
    }

    /**
     * Overloaded method will return the closest node relative to FROM. 
     * Will ignore 'from' if it equals ROGUE_VECTOR
     */
    public static GridNode GetClosestNode(Vector3 position, Vector3 from)
    {
        GridNode ret;

        bool rogue = from == ROGUE_VECTOR;
        float lastDistance = 999999.9f;
        float heuristic;

        //get a node
        ret = Instance.mNodes.Find(x => Mathf.Abs(x.mPosition.x - position.x) < 0.1f && Mathf.Abs(x.mPosition.z - position.z) < 0.1f);
      

        //if it was found and walkable do the checks
        if (ret != null && ret.mWalkable == false)
        {
            //need to check if it changed
            GridNode tmp = ret;

            //Up
            if (ret.Up != null && ret.Up.mWalkable)
            {
                if(!rogue)
                {
                    heuristic = Instance.HeuristicCost(ret.Up.mPosition, from);
                    if(heuristic < lastDistance)
                    {
                        tmp = ret.Up;
                        lastDistance = heuristic;
                    }
                }
                else
                    return ret.Up;                
            }

            //Left
            if (ret.Left != null && ret.Left.mWalkable)
            {
                if (!rogue)
                {
                    heuristic = Instance.HeuristicCost(ret.Left.mPosition, from);
                    if (heuristic < lastDistance)
                    {
                        tmp = ret.Left;
                        lastDistance = heuristic;
                    }
                }
                else
                    return ret.Left;
            }

            //Down
            if (ret.Down != null && ret.Down.mWalkable)
            {
                if (!rogue)
                {
                    heuristic = Instance.HeuristicCost(ret.Down.mPosition, from);
                    if (heuristic < lastDistance)
                    {
                        tmp = ret.Down;
                        lastDistance = heuristic;
                    }
                }
                else
                    return ret.Down;
            }

            //Right
            if (ret.Right != null && ret.Right.mWalkable)
            {
                if (!rogue)
                {
                    heuristic = Instance.HeuristicCost(ret.Right.mPosition, from);
                    if (heuristic < lastDistance)
                    {
                        tmp = ret.Right;
                        lastDistance = heuristic;
                    }
                }
                else
                    return ret.Right;
            }

            //if it didnt change then that means it is unreachable
            ret = tmp != ret ? tmp : null;
        }

        return ret;
    }

    //heuristic cost
    float HeuristicCost(Vector3 from, Vector3 to)
    {
        return Vector3.SqrMagnitude(from - to);
    }

    GridNode GetLowestFromList(Dictionary<int, float> map)
    {
        float lowest = 100000;
        GridNode ret = null;
        foreach (KeyValuePair<int, float> entry in map)
        {
            if(entry.Value < lowest)
            {
                lowest = entry.Value;
                ret = mNodes[entry.Key];
            }
        }

        map.Remove(ret.mMyIndex);
        return ret;
    }

    public List<GridNode> ReconstructPath(Dictionary<int, GridNode> cameFrom, GridNode current)
    {
        List<GridNode> path = new List<GridNode>();
        path.Add(current);

        while(cameFrom.ContainsKey(current.mMyIndex))
        {
            current = cameFrom[current.mMyIndex];
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
        Dictionary<int,GridNode> cameFrom = new Dictionary<int,GridNode>(); //The map of navigated nodes

        //cost from start along best known path
        Dictionary<int, float> g_score = new Dictionary<int, float>();
        g_score[start.mMyIndex] = 0.0f;

        //estimated total cost from start to goal through y
        Dictionary<int, float> f_score = new Dictionary<int, float>();
        f_score[start.mMyIndex] = g_score[start.mMyIndex] + Instance.HeuristicCost(start.mPosition, goal.mPosition);

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

                float tentative_g_score = g_score[current.mMyIndex] + Vector3.SqrMagnitude(current.mPosition - neighbor.mPosition);
                
                if(!openSet.Contains(neighbor) || tentative_g_score < g_score[neighbor.mMyIndex])
                {
                    cameFrom[neighbor.mMyIndex] = current;
                    g_score[neighbor.mMyIndex] = tentative_g_score;
                    f_score[neighbor.mMyIndex] = g_score[neighbor.mMyIndex] + Instance.HeuristicCost(neighbor.mPosition, goal.mPosition);

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

    public GameObject mVisualNodePrefab;
    public Material mWalkableMaterial;
    public Material mNonWalkableMaterial;

    public void GenerateGrid()
    {
        mNodes.Clear();

        int halfSize = mSize >> 1;

        Vector3 start = new Vector3(-halfSize, 0, -halfSize);
        Vector3 end = new Vector3(halfSize, 0, halfSize);

        mSnapX = EditorPrefs.GetFloat("MoveSnapX");
        mSnapY = EditorPrefs.GetFloat("MoveSnapY");
        mSnapZ = EditorPrefs.GetFloat("MoveSnapZ");

        float tmpZ = start.z;

        for (; start.x < end.x; start.x += mSnapX)
        {
            for (start.z = tmpZ; start.z < end.z; start.z += mSnapZ)
            {
                GridNode node = new GridNode(start);
                node.mMyIndex = mNodes.Count;
                mNodes.Add(node);
            }
        }

        //display them
        DisplayObjects(true);
        mDisplayGrid = true;
    }

    public GameObject [] FindGameObjectsWithLayer(string layerName) 
    {
        int layer = LayerMask.NameToLayer(layerName);
        GameObject [] allGameObjects = FindObjectsOfType<GameObject>();
        List<GameObject> retList = new List<GameObject>();
        for (var i = 0; i < allGameObjects.Length; i++)
        {
            if (allGameObjects[i].layer == layer) {
                retList.Add(allGameObjects[i]);
            }
        }

        return retList.Count > 0 ? retList.ToArray() : null;
    }

    public void DisplayObjects(bool fromGenerating = false)
    {
        //if any are displayed we first need to destroy them!
        HideObjects();

        mParent = new GameObject("Temp Grid Nodes");

        GameObject[] nonWalkables = FindGameObjectsWithLayer("NonWalkable");
        Bounds[] nonWalkableBounds = null;
        if(nonWalkables != null)
        {
            nonWalkableBounds = new Bounds[nonWalkables.Length];
            for(int i = 0; i < nonWalkables.Length; i++)
            {
                Renderer ren = nonWalkables[i].GetComponent<Renderer>();
                nonWalkableBounds[i] = ren != null ? ren.bounds : new Bounds(new Vector3(-999999, -99999, -999999), Vector3.zero);
            }
        }

        foreach(GridNode node in mNodes)
        {
            GameObject go = Instantiate(mVisualNodePrefab, node.mPosition, Quaternion.identity) as GameObject;
            go.transform.parent = mParent.transform;
            go.tag = "NavigationNode";

            //if this is the first time this node is being generated we want to see if it is inside another collider
            //this is so we can set it to non-walkable
            if(fromGenerating && nonWalkableBounds != null)
            {
                Renderer ren = go.GetComponent<Renderer>();
                for (int i = 0; i < nonWalkableBounds.Length; i++ )
                {
                    if (ren.bounds.Intersects(nonWalkableBounds[i]))
                    {
                        node.mWalkable = false;
                        ren.sharedMaterial = mNonWalkableMaterial;
                        break;
                    }
                }
            }
            else
            {
                if(node.mWalkable != true)
                {
                    Renderer ren = go.GetComponent<Renderer>();
                    ren.sharedMaterial = mNonWalkableMaterial;
                }
            }

            mObjects.Add(go);
        }
    }
    
    public void HideObjects()
    {
        if (mParent)
            DestroyImmediate(mParent);

        //make sure all previous nodes are deleted
        GameObject leakedObject = GameObject.Find("Temp Grid Nodes");
        while(leakedObject != null)
        {
            DestroyImmediate(leakedObject);
            leakedObject = GameObject.Find("Temp Grid Nodes");
        }

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
