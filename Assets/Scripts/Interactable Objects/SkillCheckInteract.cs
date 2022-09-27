using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class SkillCheckInteract : Interactable
{
    [StringInList(typeof(SkillCheckInteract), "PopulateList")]
    public string skillType;
    public float difficultyClass = 0.1f;
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




    void Awake()
    {
        interactionType = InteractionTypes.SKILL_CHECK;
        gameManager = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStateManager>();
    }

    public override bool MeetsInteractConditions()
    {
        if (gameManager != null)
        {
            return Vector3.Distance(gameManager.player.transform.position,
                transform.position) < interactionDistance
                && gameManager.canUseSkills;
        }
        return false;
    }
    public override void Interact()
    {
        gameManager.StopControl();
        bool succeedSkillCheck = gameManager.PlayerSkillCheck("Computer", 0.5f);
        if (succeedSkillCheck)
            onSuccess();
        else
            onFailed();
        gameManager.StartControl();
    }

    public abstract void onSuccess();
    public abstract void onFailed();
    public override void OptionSelected(InteractionNodeData data) { }
}
