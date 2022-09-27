using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills")]
public class SkillData : ScriptableObject 
{
    public string skillName;

    public AttributeSystem.AttributeType parentAttribute;


}
