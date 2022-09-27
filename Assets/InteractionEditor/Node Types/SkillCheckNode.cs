using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Subtegral.DialogueSystem.DataContainers;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class SkillCheckNode : InteractionNode
{
    public List<string> SkillTypes = new List<string>();
    public string skillType = "NONE";
    public float difficulty = 0.1f;
    public SkillCheckNode(InteractionGraphView graphView) : base(graphView)
    {
        nodeType = InteractionNodeTypes.SKILLCHECK;

        // read in current skill list
        string[] assetNames = AssetDatabase.FindAssets("t:SkillData");
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var skill = AssetDatabase.LoadAssetAtPath<SkillData>(SOpath);
            SkillTypes.Add("NONE");
            SkillTypes.Add(skill.skillName);
        }
    }

    public override void initPorts()
    {
        _graphView.AddStaticPort(this, "Succeed");
        _graphView.AddStaticPort(this, "Failed");

        //List<string> skillTypeNames = Enum.GetNames(typeof(SkillType)).ToList();
        SkillTypes.ForEach(skillName =>
        {
            if (skillName == "NONE")
                return;
            //Debug.Log(skillName);
            var selectButton = new Button( () => SelectSkill(skillName))
            {
                text = skillName
            };
            inputContainer.Add(selectButton);
            //inputContainer.Add(new Label("  " + skillName));   

        });

    }

    public void SelectSkill(string _skillType)
    {
        //Debug.Log("selecting skill: " + _skillType);
        skillType = _skillType;
        var input = inputContainer.ElementAt(0);
        inputContainer.Clear();
        inputContainer.Add(input);
        title = _skillType + " Skill Node";

        var port = _graphView.GeneratePort(this, Direction.Output);

        var oldLabel = port.contentContainer.Q<Label>("type");
        port.contentContainer.Remove(oldLabel);
        // Add place for difficulty
        var textField = new TextField
        {
            name = string.Empty,
            value = string.Format("{0:G}", difficulty)
        };
        textField.RegisterValueChangedCallback(evt =>
            {
                port.portName = evt.newValue;
                difficulty = float.Parse(evt.newValue);
                //Debug.Log(difficulty);
            });
            

        port.contentContainer.Add(textField);
        port.contentContainer.Add(new Label("  DC:"));
        inputContainer.Add(port);
    }
    public override void loadData(InteractionNodeData data)
    {
        GUID = data.Guid;
        skillType = data.SkillType;
        nodeName = data.nodeName;
        //Debug.Log("Read in: " + skillType + " from " + data.SkillType);
        if (data.SkillType != "NONE")
        {

            difficulty = data.SkillDifficulty;
            SelectSkill(skillType);
            //Debug.Log("Selected Loaded Skill");
        }
    }
    public override InteractionNodeData getNodeData()
    {
        var nodeData = new InteractionNodeData();
        nodeData.nodeName = "Skill Check Node";
        nodeData.Guid = GUID;
        nodeData.Position = GetPosition().position;
        nodeData.NodeType = nodeType;
        nodeData.SkillType = skillType;
        nodeData.SkillDifficulty = difficulty;
        return nodeData;
    }
    public override void addConnection(string portName)
    {
        // does nothing because by creating the node 
        // the correct ports are made
    }
}
