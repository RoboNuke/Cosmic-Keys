using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public enum BodyLocations
    {
        NONE,
        SINGLE_HANDED,
        TWO_HANDED,
        HELMET,
        UPPERBODY,
        LEGS,
        FEET,
        HANDS,
        OVERTOP,
        MASK
    }
    public enum DisplayMenu
    {
        WEAPONS,
        AID,
        APPAREL,
        KEY_ITEM,
        MISC
    }

#if UNITY_EDITOR
    public static string[] PopulateList()
    {
        List<string> _choices = new List<string>();
        string[] assetNames = AssetDatabase.FindAssets("t:SkillData");
        _choices.Clear();
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var skill = AssetDatabase.LoadAssetAtPath<SkillData>(SOpath);
            _choices.Add(skill.skillName);
        }
        return _choices.ToArray();
    }
#endif

    [System.Serializable]
    public class AtrModEntry
    {
        public AttributeSystem.AttributeType AttributeToModify;
        public int Modifier;
    }
    [System.Serializable]
    public class SkillModEntry
    {
        [StringInList(typeof(ItemData), "PopulateList")]
        public string SkillToModify;
        public float Modifier;
    }
    public string ItemName;
    public string Description;
    public Sprite ItemSprite;
    public GameObject ItemModle;
    public DisplayMenu MenuType;
    public BodyLocations BodyLocation;
    public int cost = 0;
    public List<AtrModEntry> AttributeModifiers;
    public List<SkillModEntry> SkillModifiers;

    // used for lookups
    public Hashtable AttrMods = new Hashtable();
    public Hashtable SkillMods = new Hashtable();


    public void Awake()
    {
        foreach(AtrModEntry ent in AttributeModifiers)
            AttrMods[ent.AttributeToModify] = ent.Modifier;
        foreach (SkillModEntry ent in SkillModifiers)
            SkillMods[ent.SkillToModify] = ent.Modifier;
    }
}
