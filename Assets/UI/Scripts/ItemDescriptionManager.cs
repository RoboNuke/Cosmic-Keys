using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using UnityEngine.Animations;

public class ItemDescriptionManager : MonoBehaviour
{
    private TextMeshProUGUI name;
    private TextMeshProUGUI description;
    private TextMeshProUGUI statNames;
    private TextMeshProUGUI statValues;
    private Image sprite;

    private ItemData item;
    private RectTransform pannelRect;
    
    void Awake()
    {
        name = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        description = transform.Find("Description").GetComponent<TextMeshProUGUI>();
        statNames = transform.Find("Stat Names").GetComponent<TextMeshProUGUI>();
        statValues = transform.Find("Stat Values").GetComponent<TextMeshProUGUI>();
        sprite = transform.Find("Image").GetComponent<Image>();
        pannelRect = GetComponent<RectTransform>();
        GameEventSystem.current.onHoverItem += ItemHovered;
        GameEventSystem.current.onLeaveHover += Deactivate;
        name.ForceMeshUpdate();
        description.ForceMeshUpdate();
        gameObject.SetActive(false);

        //item = Resources.Load<ItemData>("Items/Test Body Armor");
        //SetTooltip();
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void ItemHovered(ItemData _item)
    {

        gameObject.SetActive(true);
        item = _item;
        SetTooltip();
    }

    private void SetTooltip()
    {
        sprite.sprite = item.ItemSprite;
        name.SetText(item.ItemName);
        name.ForceMeshUpdate();
        description.SetText(item.Description);
        description.ForceMeshUpdate();
        Vector2 descSize = new Vector2(0f, 0f);
        descSize = description.GetRenderedValues(true);
        //Debug.Log("Desc Size: " + descSize.ToString());
        Vector2 offsets;
        offsets.x = 50;
        offsets.y = -110 - descSize.y - 10; // 110 for the description start, then the height of the description then 10 for padding
        //Debug.Log("Offsets: " + offsets.ToString());
        string attText = string.Empty;
        string attValue = string.Empty;
        foreach(ItemData.AtrModEntry att in item.AttributeModifiers)
        {
            string str = att.AttributeToModify.ToString();
            attText += "<color=#ffffff>" + str.Substring(0,1) + str.Substring(1).ToLower() + "</color>\n";
            if (att.Modifier > 0)
                attValue += "<color=#00ff00>+" + att.Modifier.ToString() + "</color>\n";
            else
                attValue += "<color=#ff0000> " + att.Modifier.ToString() + "</color>\n";
        }
        foreach(ItemData.SkillModEntry skill in item.SkillModifiers)
        {
            
            attText += "<color=#ffffff>" + skill.SkillToModify.ToString() + "</color>\n";
            if (skill.Modifier > 0)
                attValue += "<color=#00ff00>+" + skill.Modifier.ToString() + "</color>\n";
            else
                attValue += "<color=#ff0000> " + skill.Modifier.ToString() + "</color>\n";

        }
        Vector2 nameSize = new Vector2(0f,0f);
        statNames.SetText(attText);
        statValues.SetText(attValue);
        if (attText != string.Empty)
        {
            statNames.rectTransform.anchoredPosition = offsets;
            statNames.ForceMeshUpdate();
            nameSize = statNames.GetRenderedValues(false);
            //Debug.Log(nameSize);
            offsets.x += nameSize.x + 25;
            statValues.rectTransform.anchoredPosition = offsets;
        }
        //Debug.Log("Offset.y: " + offsets.y.ToString() + "  NameSize.y: " + nameSize.y.ToString());
        //Debug.Log("Verticle Height: " + (-offsets.y + 10 + nameSize.y).ToString());
        pannelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,   (-offsets.y + 10 + nameSize.y));
        //statValues.ForceMeshUpdate();
        
        
    }


    

}
