using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootRowDataManager : MonoBehaviour
{
    public Text _itemNameText;
    public GameObject _spriteHolder;
    public GameObject _indicatorHolder;
    public Sprite RightIndicator;
    public Sprite LeftIndicator;

    public ItemData item;
    private Image _itemSprite;
    private Image _indicatorSprite;
    public bool inRightPannel = false;

    void Awake()
    {
        _itemSprite = _spriteHolder.GetComponent<Image>();
        _indicatorSprite = _indicatorHolder.GetComponent<Image>();
        DeselectRow();
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

    public void SetItem(string itemName, Sprite sprite, ItemData _item, bool _inRightPannel = false)
    {
        if (_itemNameText != null)
        {
            item = _item;
            _itemNameText.text = itemName;
            _itemSprite.sprite = sprite;
            _indicatorSprite.enabled = false;
            inRightPannel = _inRightPannel;
            GetComponent<HorizontalLayoutGroup>().reverseArrangement = inRightPannel;
            ConfirmIndicator();
        }
    }

    private void ConfirmIndicator()
    {
        if (inRightPannel)
            _indicatorSprite.sprite = RightIndicator;
        else
            _indicatorSprite.sprite = LeftIndicator;
    }

    public void DebugTest()
    {
        Debug.Log("It worked");
    }

    public void SwapSides()
    {
        GameEventSystem.current.TakeLoot(this);
        inRightPannel = !inRightPannel;
        GetComponent<HorizontalLayoutGroup>().reverseArrangement = inRightPannel;
        ConfirmIndicator();
    }

    public void ToggleSelectedRow()
    {
        //_indicatorSprite.enabled = !_indicatorSprite.enabled;
        GameEventSystem.current.SelectLoot(this);
    }

    public void SelectRow()
    {
        _indicatorSprite.enabled = true;
    }

    public void DeselectRow()
    {
        _indicatorSprite.enabled = false;
    }
}
