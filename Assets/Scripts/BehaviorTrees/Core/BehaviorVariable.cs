using UnityEngine;
using System.Collections;

[System.Serializable]
public class BehaviorVariable
{
    private object mVar;        /**< The variable itself */

    private string mVariableID = "NotSet"; /**< The variable 'name' */

    /**
     * Default ctor
     */
    public BehaviorVariable(){}

    /**
     * Constructor
     */
    public BehaviorVariable(string id, object var)
    {
        mVariableID = id;
        mVar = var;
    }

#region - PROPERTIES -
   
    public System.Type VarType
    {
        get { return mVar.GetType(); }
    }

    public object Variable
    {
        get { return mVar; }
        set { mVar = value; }
    }

    public string VarName
    {
        get { return mVariableID; }
    }

 #endregion

}
