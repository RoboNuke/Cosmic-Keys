using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

//using UnityEngine.UI;


public class InteractionGraphView : GraphView
{
    public readonly Vector2 defaultNodeSize = new Vector2(150, 100);

    private InteractionNodeSearchWindow _searchWindow;

    public InteractionGraphView(EditorWindow editorWindow)
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        AddElement(GenerateEntryPointNode());
        AddSearchWindow(editorWindow);
    }

    private void AddSearchWindow(EditorWindow editorWindow)
    {
        _searchWindow = ScriptableObject.CreateInstance<InteractionNodeSearchWindow>();
        _searchWindow.init(editorWindow, this);
        nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        // could make this smarter 
        var compatablePorts = new List<Port>();

        ports.ForEach((port) =>
        {
            if (startPort != port && startPort.node != port.node)
            {
                compatablePorts.Add(port);
            }
        });

        return compatablePorts;
    }

    public void CreateNode(string nodeName, InteractionNode.InteractionNodeTypes nodeType, Vector2 position = default(Vector2))
    {
        AddElement(CreateInteractionNode(nodeName, nodeType, position));
    }

    public InteractionNode CreateInteractionNode(string nodeName,
                                                   InteractionNode.InteractionNodeTypes nodeType, 
                                                   Vector2 position = default(Vector2), 
                                                   InteractionNodeData data = null)
    {
        InteractionNode node = new InteractionNode(this);
        switch (nodeType)
        {
            case InteractionNode.InteractionNodeTypes.DIALOG:
                node = new DialogNode(this);
                break;
            case InteractionNode.InteractionNodeTypes.SKILLCHECK:
                node = new SkillCheckNode(this);
                break;
            case InteractionNode.InteractionNodeTypes.ACTION:
                node = new ActionNode(this);
                break;
            case InteractionNode.InteractionNodeTypes.ADD_ITEM:
                node = new AddItemNode(this);
                break;
            default:
                Debug.Log("Got to the default");
                break;
        }

        node.title = nodeName;
        node.GUID = Guid.NewGuid().ToString();

        // create input port
        var port = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        port.portName = "Input";
        node.inputContainer.Add(port);

        // upload style sheet specific to type?
        node.styleSheets.Add((StyleSheet)Resources.Load<StyleSheet>("Interactions/node"));

        /// Also should be specific? Not all will have multiple choices
        node.initPorts();

        // store given data
        if(data != null)
        {
            node.loadData(data);
        }

        // below is constant
        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(position, defaultNodeSize));


        return node;
    }

    public InteractionNode GenerateEntryPointNode()
    {
        var node = new InteractionNode(this);
        node.title = "START";
        node.GUID = Guid.NewGuid().ToString();
        //node.DialogText = "ENTRY POINT";
        node.EntryPoint = true;

        var port = GeneratePort(node, Direction.Output);
        port.portName = "Next";
        node.outputContainer.Add(port);

        node.capabilities &= ~Capabilities.Movable;
        node.capabilities &= ~Capabilities.Deletable;

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }

    public Port GeneratePort(InteractionNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }

    public void RemovePort(InteractionNode nody, Port byePort)
    {
        var targetEdge = edges.ToList().Where(x => x.output.portName == byePort.portName && x.output.node == byePort.node);

        if (targetEdge.Any())
        {
            var edge = targetEdge.First();
            edge.input.Disconnect(edge);
            RemoveElement(targetEdge.First());
        }
        nody.outputContainer.Remove(byePort);
        nody.RefreshPorts();
        nody.RefreshExpandedState();
    }
    public void AddStaticPort(InteractionNode node, string title)
    {
        var port = GeneratePort(node, Direction.Output);

        var oldLabel = port.contentContainer.Q<Label>("type");
        //port.contentContainer.Remove(oldLabel);

        oldLabel = new Label(title);

        //port.contentContainer.Add(new Label("  "));
        //port.contentContainer.Add(textField);

        port.portName = title;
        node.outputContainer.Add(port);

        node.RefreshExpandedState();
        node.RefreshPorts();
    }
    public void AddChoicePort(InteractionNode node, string overriddenPortName = "") // should be specific to n ode type?
    {
        var port = GeneratePort(node, Direction.Output);

        var oldLabel = port.contentContainer.Q<Label>("type");
        port.contentContainer.Remove(oldLabel);

        var outputPortCount = node.outputContainer.Query("connector").ToList().Count;


        var choicePortName = string.IsNullOrEmpty(overriddenPortName)
            ? $"Choice {outputPortCount + 1}"
            : overriddenPortName;


        var textField = new TextField
        {
            name = string.Empty,
            value = choicePortName
        };

        textField.RegisterValueChangedCallback(evt => port.portName = evt.newValue);

        port.contentContainer.Add(new Label("  "));
        port.contentContainer.Add(textField);
        var deleteButton = new Button(() => RemovePort(node, port))
        {
            text = "X"
        };
        port.contentContainer.Add(deleteButton);
        port.portName = choicePortName;
        node.outputContainer.Add(port);

        node.RefreshExpandedState();
        node.RefreshPorts();
    }
    /*
    public void AddTextInputPort(InteractionNode node, string overriddenPortName = "") // should be specific to n ode type?
    {

        var port = GeneratePort(node, Direction.Output);

        var oldLabel = port.contentContainer.Q<Label>("type");
        //port.contentContainer.Remove(oldLabel);

        oldLabel = new Label(title);

        //port.contentContainer.Add(new Label("  "));
        //port.contentContainer.Add(textField);

        port.portName = title;
        node.outputContainer.Add(port);

        node.RefreshExpandedState();
        node.RefreshPorts();
    }
    */
    }
