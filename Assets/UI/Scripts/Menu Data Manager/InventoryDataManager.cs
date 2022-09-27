using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDataManager : MenuDataManager
{
    //private GameObject player;
    //public GameObject ItemCardPrefab;
    public GameObject ItemContentContainer;

    [System.Serializable]
    public struct ScrollPannel
    {
        public ItemData.DisplayMenu name;
        public ItemData.BodyLocations BodyLocation;
        public GameObject pannel;
    };
    public List<ScrollPannel> pannels;
    private Hashtable typeToPannel = new Hashtable();

    public void Awake()
    {
        foreach( ScrollPannel pan in pannels)
        {
            if(pan.name == ItemData.DisplayMenu.APPAREL)
                typeToPannel.Add(pan.BodyLocation, pan.pannel);
            else
                typeToPannel.Add(pan.name, pan.pannel);
        }
    }


    public override void UpdateData()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        ClearInventoryMenus();

        var invSys = player.GetComponent<InventorySystem>();
        foreach (ItemData itData in invSys.items)
        {
            if (itData.MenuType == ItemData.DisplayMenu.APPAREL)
                AddItemCard(itData, (GameObject)typeToPannel[itData.BodyLocation]);
            else
                AddItemCard(itData, (GameObject)typeToPannel[itData.MenuType]);
        }
    }

    private void ClearInventoryMenus()
    {
        foreach (var key in typeToPannel.Keys)
        {
            foreach (Transform child in ((GameObject)typeToPannel[key]).transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    public void AddItemCard(ItemData itemToDisplay, GameObject ItemContentContainer)
    {
        var itemCard = Instantiate(DataCardPrefab);
        itemCard.transform.SetParent(ItemContentContainer.transform);
        itemCard.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        itemCard.GetComponent<ItemRowDataManager>().SetItem(itemToDisplay.ItemName, itemToDisplay.ItemSprite, itemToDisplay);
    }
}
