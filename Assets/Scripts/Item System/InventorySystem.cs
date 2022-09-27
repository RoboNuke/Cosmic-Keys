using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public GameObject ItemDisplayPrefab;

    public List<ItemData> items = new List<ItemData>();
    
    // what the player is currently wearing
    public ItemData headware;
    public ItemData upperBody;
    public ItemData lowerBody;
    public ItemData feet;
    public ItemData hands;
    public ItemData cloak;
    public ItemData weapon;
    private Hashtable equipted = new Hashtable();

    // component systems
    public SkillSystem parentSkillSystem;
    public AttributeSystem parentAttributeSystem;

    public void Start()
    {
        items.Add(headware);
        equipted.Add(headware.BodyLocation, headware);
        EquipItem(headware, true);
        items.Add(upperBody);
        equipted.Add(upperBody.BodyLocation, upperBody);
        EquipItem(upperBody, true);
        items.Add(lowerBody);
        equipted.Add(lowerBody.BodyLocation, lowerBody);
        EquipItem(lowerBody, true);
        items.Add(feet);
        equipted.Add(feet.BodyLocation, feet);
        EquipItem(feet, true);
        items.Add(hands);
        equipted.Add(hands.BodyLocation, hands);
        EquipItem(hands, true);
        items.Add(cloak);
        equipted.Add(cloak.BodyLocation, cloak);
        EquipItem(cloak, true);
        items.Add(weapon);
        equipted.Add(weapon.BodyLocation, weapon);
        EquipItem(weapon, true);

        GameEventSystem.current.onAddItem += AddItem;
        GameEventSystem.current.onTakeItem += TakeItem;

    }

    public void TakeItem(ItemData takeItem)
    {
        items.Remove(takeItem);
    }

    public void AddItem(ItemData newItem)
    {
        items.Add(newItem);
    }

    public void EquipItem(ItemData itemToEquip, bool init = false)
    {
        if (!init) // remove old modifiers
        {

            foreach (ItemData.AtrModEntry att in ((ItemData)equipted[itemToEquip.BodyLocation]).AttributeModifiers)
            {
                ModAttr(att, true);
            }
            foreach (ItemData.SkillModEntry skill in ((ItemData)equipted[itemToEquip.BodyLocation]).SkillModifiers)
                ModSkill(skill, true);
        }
        equipted[itemToEquip.BodyLocation] = itemToEquip;
        // Add New Modifers
        foreach (ItemData.AtrModEntry att in itemToEquip.AttributeModifiers)
        {
            //Debug.Log(att.AttributeToModify);
            ModAttr(att);
        }
        foreach (ItemData.SkillModEntry skill in ((ItemData)equipted[itemToEquip.BodyLocation]).SkillModifiers)
            ModSkill(skill);
    }
    public void ModAttr(ItemData.AtrModEntry att, bool remove = false)
    {
        if(remove)
            parentAttributeSystem.AddAttributeModifier(att.AttributeToModify, -att.Modifier);
        else
            parentAttributeSystem.AddAttributeModifier(att.AttributeToModify, att.Modifier);
    }
    public void ModSkill(ItemData.SkillModEntry skill, bool remove = false)
    {
        if (remove)
            parentSkillSystem.AddSkillMod(skill.SkillToModify, -skill.Modifier);
        else
            parentSkillSystem.AddSkillMod(skill.SkillToModify, skill.Modifier);
    }

}
