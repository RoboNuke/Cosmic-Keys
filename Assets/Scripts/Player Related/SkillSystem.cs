using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[RequireComponent(typeof(AttributeSystem))]
public class SkillSystem : MonoBehaviour
{
    private AttributeSystem attSys;

    public List<SkillData> Skills;
    public List<float> SkillPoints;
    public List<float> skillMod;
    private Hashtable NameToSkillPoints = new Hashtable();
    private Hashtable NameToSkillMod = new Hashtable();
    private Hashtable NameToData = new Hashtable();
    void Awake()
    {
        Random.InitState(42);
        Skills.Clear();
        SkillPoints.Clear();
        attSys = GetComponent<AttributeSystem>();

        SkillData[] rSkills = Resources.LoadAll<SkillData>("Skills/");

        foreach (var skill in rSkills)
        {
            Skills.Add(skill);
            SkillPoints.Add(2f);
            skillMod.Add(0f);
            NameToSkillPoints.Add(skill.skillName, 2f);
            NameToSkillMod.Add(skill.skillName, 0f);
            NameToData.Add(skill.skillName, skill);
        }
    }

    public float SkillCheck(int skillIdx) /// to remove soon
    {

        return (attSys.AttributeCheck(Skills[skillIdx].parentAttribute) 
            + SkillPoints[skillIdx]) / 100f + Random.value;
    }

    public float SkillCheck(string skillName)
    {
        return ((attSys.AttributeCheck(((SkillData)NameToData[skillName]).parentAttribute) 
            + (float)NameToSkillPoints[skillName]) / 100f + Random.value);
    } 

    public void AddSkillMod(string skillName, float modToAdd)
    {
        NameToSkillMod[skillName] = modToAdd + (float)NameToSkillMod[skillName];
        skillMod[GetIdxFromString(skillName)] = (float)NameToSkillMod[skillName];
    }
    private int GetIdxFromString(string skillName)
    {
        for(int i=0; i < Skills.Count; i++)
        {
            if (Skills[i].skillName == skillName)
                return i;
        }
        return -1;
    }

}
