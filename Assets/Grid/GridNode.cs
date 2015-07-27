using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            return index >= Grid.Instance.mNodes.Count ? null : Grid.Instance.mNodes[index].mWalkable ? Grid.Instance.mNodes[index] : null;
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
