using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class VitalsDataManager : MenuDataManager
{
    
    //public GameObject skillRowPrefab;
    public GameObject skillViewport;
    
    public override void UpdateData()
    {
        //Debug.Log("Called Update Data");
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        UpdateAttr();
        UpdateSkills();       
    }
    private void UpdateSkills()
    {
        var skillSystem = player.GetComponent<SkillSystem>();
        //Debug.Log("SkillSystem: " + (skillSystem != null).ToString());

        //Debug.Log("Skill Viewport: " + (skillViewport != null).ToString());
        //Debug.Log("Number of Skills: " + skillSystem.Skills.Count.ToString());
       
        //for (int j = 0; j < 10; j++)
        //{
            //Debug.Log("J: " + j.ToString());
            for (int i = 0; i < skillSystem.Skills.Count; i++)
            {
                //Debug.Log("I: " + i.ToString());
                var skillRow = Instantiate(DataCardPrefab);
                skillRow.transform.SetParent(skillViewport.transform);
                skillRow.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                skillRow.GetComponent<SkillRowDataManager>().SetSkillProperties(skillSystem.Skills[i].skillName, skillSystem.SkillPoints[i].ToString());
            }
        //}

    }
    public void UpdateAttr()
    {
        
        List<Text> allKids = new List<Text>(GetComponentsInChildren<Text>());
        Text attrUIText = allKids[0];
        foreach (Text t in allKids)
        {
            if (t.name == "Attribute Text")
            {
                attrUIText = t;
                break;
            }
        }
        //Debug.Log(player == null);
        var attrSys = player.GetComponent<AttributeSystem>();
        List<string> attrTypes = Enum.GetNames(typeof(AttributeSystem.AttributeType)).ToList();
        //Debug.Log("First: " + attrTypes[0]);
        string attrText = "";
        foreach(string attr in attrTypes)
        {
            attrText += attr + ": " + attrSys.GetBaseAttribute((AttributeSystem.AttributeType)Enum.Parse(
                typeof(AttributeSystem.AttributeType), attr)) + "\n";
            //Debug.Log("Adding " + attr + " value to menu");
        }
        attrUIText.text = attrText;
        //Debug.Log(attrText);
        
    }

}
