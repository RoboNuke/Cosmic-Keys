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


public class InteractionGraph : EditorWindow
{
    public InteractionGraphView _graphView;
    public string _fileName = "New Interaction Graph";

    [MenuItem("Graph/Interaction Graph")]
    public static void OpenDialogGraphWindow()
    {
        var window = GetWindow<InteractionGraph>();
        window.titleContent = new GUIContent("Interaction Graph");

    }

    public void OnEnable()
    {
        GenerateGraphView();
        GenerateToolbar();
        GenerateMiniMap();
    }

    private void GenerateMiniMap()
    {
        var minimap = new MiniMap { anchored = true };
        minimap.SetPosition(new Rect(10, 30, 200, 140));
        _graphView.Add(minimap);
    }

    private void GenerateGraphView()
    {

        _graphView = new InteractionGraphView(this);
        _graphView.name = "Interaction Graph";


        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);

    }

    private void GenerateToolbar()
    {

        Toolbar toolbar = new Toolbar();

        var fileNameTextField = new TextField("File Name:");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
        toolbar.Add(fileNameTextField);

        toolbar.Add(new Button(() => RequestDataOperation(true)) { text = "Save Data" });
        toolbar.Add(new Button(() => RequestDataOperation(false)) { text = "Load Data" });


        //var nodeCreationButton = new Button(() => { _graphView.CreateNode("Dialog Node"); });
        //nodeCreationButton.text = "Create Node";
        //toolbar.Add(nodeCreationButton);

        rootVisualElement.Add(toolbar);
    }

    private void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            EditorUtility.DisplayDialog("Invalid file name!", "Please enter a valid file name.", "OK");
            return;
        }
        // Return when save Utility updated
        var saveUtility = InteractionGraphSaveUtility.GetInstance(_graphView);

        if (save)
        {
            saveUtility.SaveGraph(_fileName);
        }
        else
        {
            saveUtility.LoadGraph(_fileName);
        }
        
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
