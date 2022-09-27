using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInteract : Interactable
{

    public Canvas DialogWindow;
    public InteractionContainer intContainer;

    private IInteractionGraph _IGraph;
    private InteractionGraphReader _intGraph;
    private InteractionNodeData _currentNode;

    public void Awake()
    {
        interactionType = InteractionTypes.DIALOG;
        gameManager = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStateManager>();
        DialogWindow.enabled = false;
        _intGraph = new InteractionGraphReader(intContainer);
        Debug.Log(_intGraph);
        _currentNode = _intGraph.GetFirstNode();
        Debug.Log(_currentNode);
        _IGraph = DialogWindow.GetComponent<IInteractionGraph>();
        Debug.Log(_IGraph);
        //Debug.Log("First Node: " + _currentNode.nodeName);
    }

    public void ResetDialog()
    {
        _currentNode = _intGraph.GetFirstNode();
    }
    public override void Interact()
    {
        gameManager.StopControl();
        gameManager.DisableUI();
        DialogWindow.enabled = true;
        ResetDialog();
        UpdateOptions();
    }

    private void UpdateOptions()
    {
        Debug.Log(_IGraph);
        _IGraph.UpdateMessage(_currentNode.nodeName);
        Debug.Log("Updated message");
        _IGraph.ClearOptions();
        Debug.Log("Cleared Options");
        for (int i = 0; i < _currentNode.Children.Count; i++)
        {
            Debug.Log(i);
            _IGraph.AddSelectableObject(_currentNode.PortNames[i], 
                this, 
                _currentNode.Children[i]
            );
        }
    }
    public override void OptionSelected(InteractionNodeData node)
    {
        _currentNode = gameManager.ProcessInteractionNode(node);

        if(_currentNode == null)
        {
            DialogWindow.enabled = false;
            gameManager.StartControl();
            gameManager.EnableUI();
            return;
        }
        UpdateOptions();
    }

    public override bool MeetsInteractConditions()
    {
        if (gameManager != null)
        {
            return Vector3.Distance(gameManager.player.transform.position, 
                transform.position) < interactionDistance 
                && gameManager.canEnterDialog;
        }
        else
        {
            return false;
        }
    }

    public void handleDialogOption(int childIdx)
    {
        // some logic to update the display
        InteractionNodeData nextNode = _currentNode.Children[childIdx];


    }

    public void updateDialogDisplay(InteractionNodeData node)
    {
        // some logic here

    }
    
}
