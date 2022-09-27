using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillRowDataManager : MonoBehaviour
{
    Text skillNameText;
    Text skillValueText;

    void Awake()
    {
        List<Text> allKids = new List<Text>(GetComponentsInChildren<Text>());
        skillNameText = allKids[0];
        skillValueText = allKids[1];
    }

    public void SetSkillProperties(string skillName, string skillValue)
    {
        if (skillNameText != null)
        {
            skillNameText.text = skillName;
            skillValueText.text = skillValue;
        }
    }
}
