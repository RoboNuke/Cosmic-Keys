using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractionNodeSearchWindow : ScriptableObject, ISearchWindowProvider
{
    private InteractionGraphView _graphView;
    private EditorWindow _window;

    public void init(EditorWindow window, InteractionGraphView graphView)
    {
        _graphView = graphView;
        _window = window;

        _indentationIcon = new Texture2D(1, 1);
        _indentationIcon.SetPixel(0, 0, new Color(0, 0, 0, 0));
        _indentationIcon.Apply();
    }

    private Texture2D _indentationIcon;
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var tree = new List<SearchTreeEntry>
        {

            new SearchTreeGroupEntry(new GUIContent("Create Elements"), 0),
            new SearchTreeEntry(     new GUIContent("Dialog Node", _indentationIcon))
            {
                level = 1,
                userData = new DialogNode(_graphView)
            },
            new SearchTreeEntry( new GUIContent("Skill Check Node", _indentationIcon))
            {
                level = 1,
                userData = new SkillCheckNode(_graphView)
            },
            new SearchTreeEntry( new GUIContent("Action Node", _indentationIcon))
            {
                level = 1,
                userData = new ActionNode(_graphView)
            },
            new SearchTreeEntry( new GUIContent("Get Item Node", _indentationIcon))
            {
                level = 1,
                userData = new AddItemNode(_graphView)
            }
            //new SearchTreeEntry(new GUIContent("Hello World"))
        };
        return tree;

    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        var worldMousePosition = _window.rootVisualElement.ChangeCoordinatesTo(_window.rootVisualElement.parent,
            context.screenMousePosition - _window.position.position);
        var localMousePosition = _graphView.contentViewContainer.WorldToLocal(worldMousePosition);
        switch (SearchTreeEntry.userData)
        {
            case DialogNode interactionNode:
                _graphView.CreateNode("Dialog Node", InteractionNode.InteractionNodeTypes.DIALOG, localMousePosition);
                return true;
            case SkillCheckNode interactionNode:
                _graphView.CreateNode("Skill Check Node", InteractionNode.InteractionNodeTypes.SKILLCHECK, localMousePosition);
                return true;
            case ActionNode interactionNode:
                _graphView.CreateNode("Skill Check Node", InteractionNode.InteractionNodeTypes.ACTION, localMousePosition);
                return true;
            case AddItemNode interactionNode:
                _graphView.CreateNode("Give Item Node", InteractionNode.InteractionNodeTypes.ADD_ITEM, localMousePosition);
                return true;
            default:
                return true;
        }

    }
}
