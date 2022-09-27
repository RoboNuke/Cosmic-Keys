using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.UIElements;

[System.Serializable]
public class InteractionNodeData
{
    public string nodeName;
    public InteractionNode.InteractionNodeTypes NodeType;
    public string Guid;
    public Vector2 Position;
    

    // realtime stuff
    public List<InteractionNodeData> Children;
    public List<string> PortNames;
    // dialog stuff
    public string DialogText;

    // skill stuff
    public string SkillType;
    public float SkillDifficulty;

    // action stuff
    public string ActionName;
    public int ActionIdx;

    // Item Drop Stuff
    public List<ItemData> ItemsToGive;
    public Hashtable ItemNameToCount;
}