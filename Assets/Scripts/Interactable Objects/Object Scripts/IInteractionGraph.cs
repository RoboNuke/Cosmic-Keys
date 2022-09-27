using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractionGraph 
{
    void UpdateMessage(string message);
    void AddSelectableObject(string message, Interactable manager, InteractionNodeData optionNode);
    void ClearOptions();
}
