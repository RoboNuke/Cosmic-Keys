using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionInteraction : MonoBehaviour
{
    protected GameStateManager gameManager;
    // Start is called before the first frame update
    public void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStateManager>();
    }
    public abstract void TakeAction(string actionName);
}
