using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : Interactable
{
    //public Canvas LootMenu;

    public List<ItemData> ItemsInLoot = new List<ItemData>();
    //public Animation anim;
    public string Narration = "";

    public void Awake()
    {
        interactionType = InteractionTypes.LOOTING;
        gameManager = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStateManager>();
        
    }

  
    public override void Interact()
    {
        gameManager.StopControl();
        GameEventSystem.current.NewNarration(Narration, true);
        GameEventSystem.current.OpenLootMenu(ItemsInLoot, this);

        transform.parent.GetComponent<Animation>().Play("Crate_Open");
        //GameEventSystem.current.onCloseLootMenu += CloseLootMenu;
    }

    public void CloseLootMenu()
    {
        Debug.Log("Calling close in LootBox");
        gameManager.StartControl();
        transform.parent.GetComponent<Animation>().Play("Crate_Close");
        //GameEventSystem.current.onCloseLootMenu -= CloseLootMenu;
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
    public override void OptionSelected(InteractionNodeData node) { }
}
