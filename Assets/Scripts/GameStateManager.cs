using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public bool canEnterDialog = true;
    public bool canControlCamera = true;
    public bool canUseSkills = true;
    public bool canMove = true;
    public GameObject player;
    public List<ActionInteraction> ActionObjects;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Fix for reboot");
        player = GameObject.FindGameObjectWithTag("Player");
    }
   public void StopControl()
    {
        canControlCamera = false;
        canMove = false;
    }
    public void StartControl()
    {
        canControlCamera = true;
        canMove = true;
        
    }
    public void DisableUI()
    {
        GetComponent<UIManager>().DisableUI();
    }

    public void EnableUI()
    {
        GetComponent<UIManager>().EnableUI();
    }

    public bool PlayerSkillCheck(string skillType, float dc)
    {
        float roll = 0f;
        SkillSystem skillSystem = player.GetComponent<SkillSystem>();
        for (int i = 0; i < skillSystem.Skills.Count; i++)
        {
            if (skillSystem.Skills[i].skillName == skillType)
            {
                roll = skillSystem.SkillCheck(i);
                break;
            }
        }

        return (roll > dc);
            
    }
    public InteractionNodeData ProcessInteractionNode(InteractionNodeData node)
    {
        if(node.NodeType == InteractionNode.InteractionNodeTypes.SKILLCHECK)
        {
            if(PlayerSkillCheck(node.SkillType, node.SkillDifficulty))
                return (checkChild(node.Children[0]));
            else
                return (checkChild(node.Children[1]));
        }
        else if(node.NodeType == InteractionNode.InteractionNodeTypes.ACTION)
        {
            //Debug.Log("Call Action");
            ActionObjects[node.ActionIdx].TakeAction(node.ActionName);
            return (ProcessInteractionNode(node.Children[0]));
        }

        else if (node.nodeName == "Finish")
        {
            return null;
        }
        //Debug.Log("Dialog Node Found");
        return node;
    }

    private InteractionNodeData checkChild(InteractionNodeData node)
    {
        if (node.NodeType == InteractionNode.InteractionNodeTypes.DIALOG)
            return node;
        else
            return ProcessInteractionNode(node);
    }
}
