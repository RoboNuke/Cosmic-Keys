using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HUDDataManager : MonoBehaviour
{
    private Text healthText;
    private Text narration;
    public ScrollRect scrollRect;
    public void Start()
    {
        GameEventSystem.current.onHealthChange += UpdateHealth;
        GameEventSystem.current.onNewNarration += AddNarration;
        List<Text> allKids = new List<Text>(GetComponentsInChildren<Text>());

        foreach (Text t in allKids)
        {
            if (t.name == "HP Tracker Text")
            {
                healthText = t;
            }
            if (t.name == "Narration")
            {
                narration = t;
            }
        }
        narration.text = "";
        //Debug.Log("HUD up");
    }

    private void UpdateHealth(float currentHP, float maxHP)
    {
        healthText.text = currentHP.ToString() + "/" + maxHP.ToString();
    }

    private void AddNarration(string newNar)
    {
        if (newNar != "")
        {
            if (narration.text != "")
                narration.text += "\n";
            narration.text += newNar;
            scrollRect.normalizedPosition = new Vector2(0, 0);
        }
    }

}
