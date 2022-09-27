using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class InteractionNPC : MonoBehaviour, IInteractionGraph
{
    public Text messageText;
    public GameObject optionPrefab;
    public Canvas DialogWindow;
    public GameObject optionPannel;

    public void Awake()
    {

    }
    public void UpdateMessage(string message)
    {
        messageText.text = message;
    }
    public void AddSelectableObject(string message, Interactable manager, InteractionNodeData optionNode)
    {
        var option = Instantiate(optionPrefab);
        // make pannel its parent and scale correctly
        option.transform.SetParent(optionPannel.transform);
        option.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        // set message text
        option.GetComponentsInChildren<Text>()[0].text = message;
        // set button callback
        option.GetComponentsInChildren<Button>()[0].onClick.AddListener(
            () => manager.OptionSelected(optionNode)
        );
        //option.GetComponentsInChildren<Button>()[0].interactable = true;
    }

    public void ClearOptions()
    {
        foreach (Transform child in optionPannel.transform)
        {
            Destroy(child.gameObject);
        }

    }
}
