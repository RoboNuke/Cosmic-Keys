using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDataManager : MenuDataManager
{
    public Canvas LootMenu;
    public GameObject LeftPannel;
    public GameObject RightPannel;
    public GameObject Player;
    public List<ItemData> ItemsInLoot = new List<ItemData>();
    private List<ItemData> PlayerItems = new List<ItemData>();
    private LootRowDataManager currentRow;
    private LootBox lootBox;

    public void Awake()
    {
        GameEventSystem.current.onSelectLoot += NewLootSelected;
        GameEventSystem.current.onTakeLoot += SwapRow;
        GameEventSystem.current.onOpenLootMenu += OpenMenu;
        GameEventSystem.current.onCloseLootMenu += CloseMenu;
        UpdateData();
        LootMenu.enabled = false;
    }

    public void OpenMenu(List<ItemData> itemsToAdd, LootBox lb)
    {
        ItemsInLoot = itemsToAdd;
        PlayerItems = Player.GetComponent<InventorySystem>().items;
        lootBox = lb;
        UpdateData();
        LootMenu.enabled = true;
    }

    public void CloseMenu()
    {
        //Debug.Log("Called Close Menu in Data Manager");
        lootBox.ItemsInLoot = ItemsInLoot; // reset item for if player returns
        LootMenu.enabled = false;
        lootBox.CloseLootMenu();
    }

    public void TakeAllItems()
    {
        Debug.Log("Called Take All Items");
        List<LootRowDataManager> rows = new List<LootRowDataManager>();
        foreach (Transform row in RightPannel.transform)
            rows.Add(row.gameObject.GetComponent<LootRowDataManager>());
        foreach (var row in rows)
            SwapRow(row);
    }

    public void SwapRow(LootRowDataManager row)
    {
        if(row.inRightPannel) // move to left
        {
            row.transform.SetParent(LeftPannel.transform);
            row.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            ItemsInLoot.Remove(row.item);
            PlayerItems.Add(row.item);
        }
        else
        {
            row.transform.SetParent(RightPannel.transform);
            row.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            PlayerItems.Remove(row.item);
            ItemsInLoot.Add(row.item);
        }

    }

    public void NewLootSelected(LootRowDataManager row)
    {
        if (currentRow != null)
            currentRow.DeselectRow();

        currentRow = row;
        currentRow.SelectRow();
    }

    public override void UpdateData()
    {

        ClearPannels();

        
        foreach (ItemData itData in ItemsInLoot)
        {

            AddRowCard(itData, RightPannel, true);
        }

        foreach(ItemData itData in PlayerItems)
        {
            AddRowCard(itData, LeftPannel, false);
        }
    }
    
    private void ClearPannels()
    {
        foreach (Transform child in LeftPannel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in RightPannel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void AddRowCard(ItemData itemToDisplay, GameObject ItemContentContainer, bool inLoot)
    {
        var itemCard = Instantiate(DataCardPrefab) as GameObject;
        itemCard.transform.SetParent(ItemContentContainer.transform);
        itemCard.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        itemCard.GetComponent<LootRowDataManager>().SetItem(itemToDisplay.ItemName, itemToDisplay.ItemSprite, itemToDisplay, inLoot);
    }
}
