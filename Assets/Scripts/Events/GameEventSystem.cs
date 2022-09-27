using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem current;
    bool doubleClicked = false;

    public void Awake()
    {
        current = this;
    }

    public event Action<float, float> onHealthChange;
    public void HealthChange(float currentHealth, float maxHealth)
    {
        if (onHealthChange != null)
            onHealthChange(currentHealth, maxHealth);
    }


    public event Action<ItemData> onAddItem;
    public void AddItem(ItemData newItem)
    {
        if(onAddItem != null)
        {
            onAddItem(newItem);
        }
    }

    public event Action<ItemData> onTakeItem;
    public void TakeItem(ItemData takeItem)
    {
        if(onTakeItem != null)
        {
            onTakeItem(takeItem);
        }
    }

    // Loot Menu Stuff
    public event Action<LootRowDataManager> onSelectLoot;
    public void SelectLoot(LootRowDataManager row)
    {
        if (onSelectLoot != null)
            onSelectLoot(row);
    }

    public event Action<LootRowDataManager> onTakeLoot;
    public void TakeLoot(LootRowDataManager row)
    {
        if (onTakeLoot != null)
            onTakeLoot(row);
    }

    public event Action<List<ItemData>, LootBox> onOpenLootMenu;
    public void OpenLootMenu(List<ItemData> itemsToLoot, LootBox lb)
    {
        if (onOpenLootMenu != null)
            onOpenLootMenu(itemsToLoot, lb);
    }

    public event Action onCloseLootMenu;
    public void CloseLootMenu()
    {
        if (onCloseLootMenu != null)
            onCloseLootMenu();
    }

    public event Action<ItemData> onHoverItem;
    public void HoverItem(ItemData item)
    {
        if (onHoverItem != null)
            onHoverItem(item);
    }

    public event Action onLeaveHover;
    public void LeaveHover()
    {
        if (onHoverItem != null)
            onLeaveHover();
    }

    public event Action<string> onNewNarration;
    public void NewNarration(string nar)
    {
        if ((doubleClicked || false) && onNewNarration != null)
            onNewNarration(nar);
    }

    public void NewNarration(string nar, bool displayAnyway)
    {
        if ((doubleClicked || displayAnyway) && onNewNarration != null)
            onNewNarration(nar);
    }


    public void CheckDoubleClick(BaseEventData ptData)
    {
        doubleClicked = (((PointerEventData)ptData).clickCount > 1);

    }
        
}
