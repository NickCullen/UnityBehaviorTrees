using UnityEngine;
using System.Collections.Generic;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class BTNode
{
	public List<BTNode> Children = new List<BTNode>();


	#if UNITY_EDITOR
	void Render()
	{

	}

	#endif

}
