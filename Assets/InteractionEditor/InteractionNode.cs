using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InteractionNode : Node
{
    public enum InteractionNodeTypes 
    {
        DIALOG,
        SKILLCHECK,
        ACTION,
        ADD_ITEM
    }
    public string nodeName;

    public InteractionNodeTypes nodeType;

    public string GUID;

    public bool EntryPoint = false;

    public InteractionGraphView _graphView;


    public InteractionNode(InteractionGraphView graphView)
    {
        _graphView = graphView;
    }

    public virtual void initPorts() { }

    public virtual InteractionNodeData getNodeData()
    {
        return new InteractionNodeData();
    }
    
    public virtual void loadData(InteractionNodeData data)
    {
        
    }

    public virtual void addConnection(string portName) { }
}
