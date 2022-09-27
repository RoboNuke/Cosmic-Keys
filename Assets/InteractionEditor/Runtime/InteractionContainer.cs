using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionContainer : ScriptableObject
{
    public List<NodeLinkData> NodeLinks = new List<NodeLinkData>();
    public List<InteractionNodeData> InteractionNodeData = new List<InteractionNodeData>();

}
