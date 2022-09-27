using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Subtegral.DialogueSystem.DataContainers;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class ActionNode : InteractionNode
{
    public string actionName;
    public int actionIdx;

    public ActionNode(InteractionGraphView graphView) : base(graphView)
    {
        nodeType = InteractionNodeTypes.ACTION;
    }

    public override void initPorts()
    {
        // output node
        _graphView.AddStaticPort(this, "Output");

        // Action Index Input
        var intField = new TextField("Action Idx:");
        intField.tooltip = "The index of the object in the object list used at runtime.";
        intField.RegisterValueChangedCallback(evt =>
        {
            int newValue;
            if(int.TryParse(evt.newValue, out newValue))
            {
                actionIdx = newValue;
            }
            else
            {
                intField.value = "0";
            }

        });
        intField.SetValueWithoutNotify("0");
        mainContainer.Add(intField);
        // Action Name Input
        var textField = new TextField("");
        textField.tooltip = "Keyword used to signal to object at runtime what action to take.";
        title = "Action Node";
        actionName = "Action Type";
        textField.RegisterValueChangedCallback(evt =>
        {
            actionName = evt.newValue;
            title = evt.newValue + " Action Node";
        });
        textField.SetValueWithoutNotify(actionName);
        mainContainer.Add(textField);
    }

    public override InteractionNodeData getNodeData()
    {
        var nodeData = new InteractionNodeData();
        nodeData.nodeName = title;
        nodeData.Guid = GUID;
        nodeData.Position = GetPosition().position;
        nodeData.NodeType = nodeType;
        nodeData.ActionName = actionName;
        nodeData.ActionIdx = actionIdx;
        return nodeData;
    }

    public override void loadData(InteractionNodeData data)
    {
        GUID = data.Guid;
        actionName = data.ActionName;
        nodeName = data.nodeName;
        actionIdx = data.ActionIdx;

    }

    public override void addConnection(string portName)
    {
        // should not have connections
    }
}
