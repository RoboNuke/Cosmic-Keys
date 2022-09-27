using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameStateManager gameManager;
    public Canvas HUD;
    //public Canvas VitalMenu;
    public List<Canvas> UIMenus;
    private Canvas currentUI;

    public void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStateManager>();
        foreach (Canvas menu in UIMenus)
            menu.enabled = false;
        HUD.enabled = true;
        currentUI = HUD;

    }
    public void DisableUI()
    {
        currentUI.enabled = false;
    }
    public void EnableUI()
    {
        currentUI.enabled = true;
    }
    public void TransitionUI(int idx = -1)
    {
        currentUI.enabled = false;
        if (idx == -1)
        {
            currentUI = HUD;
            gameManager.StartControl();
            HUD.enabled = true;
            return;
        }
        UIMenus[idx].GetComponent<MenuDataManager>().UpdateData();
        currentUI = UIMenus[idx];
        gameManager.StopControl();
        HUD.enabled = false;
        currentUI.enabled = true;

    
    }
    
}
