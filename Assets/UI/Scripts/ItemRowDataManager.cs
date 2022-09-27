using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRowDataManager : MonoBehaviour
{
    public Text _itemNameText;
    public GameObject _spriteHolder;
    private Image _itemSprite;
    private ItemData item;

    void Awake()
    {
        _itemSprite = _spriteHolder.GetComponent<Image>();
    }

    public void SetItem(string itemName, Sprite sprite, ItemData _item)
    {
        if (_itemNameText != null)
        {
            _itemNameText.text = itemName;
            _itemSprite.sprite = sprite;
            item = _item;
        }
    }

    public void MouseEntered()
    {
        GameEventSystem.current.HoverItem(item);
    }

    public void MouseExited()
    {
        //Debug.Log("Called leave");
        GameEventSystem.current.LeaveHover();
    }
}
