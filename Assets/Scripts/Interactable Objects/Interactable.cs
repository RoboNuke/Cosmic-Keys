using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public enum InteractionTypes
    {
        SKILL_CHECK,
        DIALOG,
        LOOTING,
    }

    public InteractionTypes interactionType;
    public GameStateManager gameManager;
    public float interactionDistance;

    public abstract void Interact();
    public abstract bool MeetsInteractConditions();
    public abstract void OptionSelected(InteractionNodeData node);

    public void OnMouseDown()
    {
        Debug.Log("Clicked here");
        if (MeetsInteractConditions())
        {
            Debug.Log("It met the conditions");
            Interact();
        }
    }
}
