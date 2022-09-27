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

public class DialogNode : InteractionNode
{


    public string DialogText;

    public DialogNode(InteractionGraphView graphView) : base(graphView)
    {
        nodeType = InteractionNodeTypes.DIALOG;
    }

    public override void initPorts()
    {
        var button = new Button(() => { _graphView.AddChoicePort(this); });
        button.text = "New Choice";
        titleContainer.Add(button);

        var textField = new TextField(string.Empty);
        DialogText = title;
        textField.RegisterValueChangedCallback(evt =>
        {
            DialogText = evt.newValue;
            title = evt.newValue;
        });
        textField.SetValueWithoutNotify(title);
        mainContainer.Add(textField);

    }
    public override void loadData(InteractionNodeData data)
    {
        GUID = data.Guid;
        DialogText = data.DialogText;
    }
    public override InteractionNodeData getNodeData()
    {
        var nodeData = new InteractionNodeData();
        nodeData.nodeName = DialogText;
        nodeData.Guid = GUID;
        nodeData.Position = GetPosition().position;
        nodeData.DialogText = DialogText;
        nodeData.NodeType = nodeType;
        return nodeData;
    }

    public override void addConnection(string portName)
    {
        _graphView.AddChoicePort(this, portName);
    }
}
