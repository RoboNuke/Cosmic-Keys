using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


public class InteractionGraphSaveUtility
{
    
    private InteractionGraphView _targetGraphView;
    private InteractionContainer _containerCache;

    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<InteractionNode> Nodes => _targetGraphView.nodes.ToList().Cast<InteractionNode>().ToList();


    public static InteractionGraphSaveUtility GetInstance(InteractionGraphView targetGraphView)
    {
        return new InteractionGraphSaveUtility { _targetGraphView = targetGraphView };
    }

    public void SaveGraph(string filename)
    {
        
        if (!Edges.Any()) return;

        var interactionContainer = ScriptableObject.CreateInstance<InteractionContainer>();

        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();
        for (var i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as InteractionNode;
            var inputNode = connectedPorts[i].input.node as InteractionNode;

            interactionContainer.NodeLinks.Add(new NodeLinkData
            {
                BaseNodeGuid = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGuid = inputNode.GUID
            });
        }

        foreach (var node in Nodes.Where(Nodes => !Nodes.EntryPoint))
        {
            interactionContainer.InteractionNodeData.Add(node.getNodeData());
        }

        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets", "Resources");

        if (!AssetDatabase.IsValidFolder("Assets/Resources/Interactions"))
            AssetDatabase.CreateFolder("Assets/Resources", "Interactions");

        AssetDatabase.CreateAsset(interactionContainer, $"Assets/Resources/Interactions/{filename}.asset");
        AssetDatabase.SaveAssets();
        
    }

    public void LoadGraph(string filename)
    {
        // EditorUtility.DisplayDialog("FilePath", $"Assets/Resources/Dialogs/{filename}.asset", "OK");
        
        _containerCache = (InteractionContainer)Resources.Load<InteractionContainer>($"Interactions/{filename}");

        if (_containerCache == null)
        {
            EditorUtility.DisplayDialog("File Not Found", "Target dialogue graph does not exist!", "OK");
            Debug.Log($"Assets/Resources/Dialogs/{filename}.asset");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
        
    }
    
    private void ConnectNodes()
    {
        for (var i = 0; i < Nodes.Count; i++)
        {
            var connections = _containerCache.NodeLinks.Where(x => x.BaseNodeGuid == Nodes[i].GUID).ToList();
            for (var j = 0; j < connections.Count; j++)
            {
                var targetNodeGuid = connections[j].TargetNodeGuid;
                var targetNode = Nodes.First(x => x.GUID == targetNodeGuid);
                LinkNodes(Nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);
                targetNode.SetPosition(new Rect(
                    _containerCache.InteractionNodeData.First(x => x.Guid == targetNodeGuid).Position,
                    _targetGraphView.defaultNodeSize
                ));
            }
        }
    }

    private void LinkNodes(Port output, Port input)
    {
        var tempEdge = new Edge
        {
            output = output,
            input = input
        };
        tempEdge.input.Connect(tempEdge);
        tempEdge.output.Connect(tempEdge);
        _targetGraphView.Add(tempEdge);
    }
    private void CreateNodes()
    {
        foreach (var nodeData in _containerCache.InteractionNodeData)
        {
            var tempNode = _targetGraphView.CreateInteractionNode(nodeData.nodeName, nodeData.NodeType, default(Vector2), nodeData);
            tempNode.GUID = nodeData.Guid;
            _targetGraphView.AddElement(tempNode);

            var nodePorts = _containerCache.NodeLinks.Where(x => x.BaseNodeGuid == nodeData.Guid).ToList();
            nodePorts.ForEach(x => tempNode.addConnection(x.PortName));
        }
    }
    private void ClearGraph()
    {
        Nodes.Find(x => x.EntryPoint).GUID = _containerCache.NodeLinks[0].BaseNodeGuid;

        foreach (var node in Nodes)
        {
            if (node.EntryPoint) continue;

            Edges.Where(x => x.input.node == node).ToList()
                .ForEach(edge => _targetGraphView.RemoveElement(edge));

            _targetGraphView.RemoveElement(node);
        }
    }
}
