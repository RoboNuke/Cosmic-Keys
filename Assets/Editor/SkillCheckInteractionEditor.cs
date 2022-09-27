using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(LockpickDoor))]
//[CanEditMultipleObjects]
public class SkillCheckInteractionEditor : Editor
{
    /*
    SerializedProperty skillType;
    private int _choiceIndex = 0;
    private List<string> _choices = new List<string>();

    void OnEnabled()
    {

        PopulateList();
        skillType = serializedObject.FindProperty("skillType");
        //_choiceIndex = Array.IndexOf(_choices, skillType.stringValue);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();
        _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices.ToArray(), "Skill");
        if (_choiceIndex < 0)
            _choiceIndex = 0;
        skillType.stringValue = _choices[_choiceIndex];

        serializedObject.ApplyModifiedProperties();
    }


    void PopulateList()
    {
        string[] assetNames = AssetDatabase.FindAssets("t:SkillData");
        _choices.Clear();
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var skill = AssetDatabase.LoadAssetAtPath<SkillData>(SOpath);
            _choices.Add(skill.skillName);
        }
    }
    */
}
