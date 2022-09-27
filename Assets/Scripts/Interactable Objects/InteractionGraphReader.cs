using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InteractionGraphReader 
{
    private InteractionContainer intContainer;

    private List<InteractionNodeData> nodes = new List<InteractionNodeData>();
    private InteractionNodeData firstNode;

    public InteractionGraphReader(InteractionContainer _intContainer)
    {
        intContainer = _intContainer;

        CreateNodes();
        //DisplayNodes();

    }
    public InteractionNodeData GetFirstNode()
    {
        return firstNode;
    }

    private void CreateNodes()
    {
        foreach (var nodeData in intContainer.InteractionNodeData)
        {
            nodes.Add(nodeData);
        }

        foreach (var node in nodes)
        {
            AddNodeChildren(node);
        }

        var firstNodeGuid = intContainer.NodeLinks.Where(x=>x.PortName == "Next").ToList()[0].TargetNodeGuid;
        firstNode = nodes.First(x => x.Guid == firstNodeGuid);

    }   

    private void AddNodeChildren(InteractionNodeData node)
    {
        node.Children.Clear();
        node.PortNames.Clear();
        var connections = intContainer.NodeLinks.Where(x => x.BaseNodeGuid == node.Guid).ToList();
        for (int i = 0; i < connections.Count; i++)
        {
            var targetNodeGuid = connections[i].TargetNodeGuid;
            node.Children.Add(nodes.First(x => x.Guid == targetNodeGuid));
            node.PortNames.Add(connections[i].PortName);
        }
    }

    private void DisplayNodes()
    {
        foreach (var node in nodes)
        {
            Debug.Log(node.nodeName);
            foreach (var childNode in node.Children)
            {
                
                Debug.Log("--" + childNode.nodeName);
            }
        }
    }
}
