using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[CustomEditor(typeof(SkillSystem))]
public class SkillSystemEditor : Editor
{
    public List<string> skillNames;
    public int _choiceIndex;

    

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SkillSystem ss = (SkillSystem)target;
        
        PopulateList(ss);
       
    }


    void PopulateList(SkillSystem ss)
    {
        string[] assetNames = AssetDatabase.FindAssets("t:SkillData");
        ss.Skills.Clear();
        ss.SkillPoints.Clear();
        //skillNames.Clear();
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var skill = AssetDatabase.LoadAssetAtPath<SkillData>(SOpath);
            ss.Skills.Add(skill);
            ss.SkillPoints.Add(2);
        //    skillNames.Add(skill.skillName);
        }
    }
    
}

