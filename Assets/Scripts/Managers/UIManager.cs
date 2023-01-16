using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Serializable]
    public class Screen
    {
        public UIScreenType type = UIScreenType.None;
        public GameObject gameObject;
        public bool isStart;
    }

    public Screen[] screens;

    private void Start()
    {
        foreach (Screen screen in screens)
        {
            if (screen.isStart)
            {
                ToggleScreen(screen.type, true);
            }
            else
            {
                ToggleScreen(screen.type, false);
            }
        }
    }
    private void OnEnable()
    {
        GameEventManager.ShowWinScreen.AddListener(OnShowWinScreen);
        GameEventManager.PlayerDeath.AddListener(OnShowLose);
        GameEventManager.ToggleScreen.AddListener(OnToggleScreen);
    }

    private void OnDisable()
    {
        GameEventManager.ShowWinScreen.RemoveListener(OnShowWinScreen);
        GameEventManager.PlayerDeath.RemoveListener(OnShowLose);
        GameEventManager.ToggleScreen.RemoveListener(OnToggleScreen);
    }

    private void ToggleScreen(UIScreenType type, bool isOn)
    {
        for (int i = 0; i < screens.Length; i++)
        {
            if (screens[i].type == type)
            {
                screens[i].gameObject.SetActive(isOn);
                break;
            }
        }
    }

    protected void OnToggleScreen(UIScreenType type, bool isOn)
    {
        ToggleScreen(type, isOn);
    }

    protected void OnShowLose()
    {
        ToggleScreen(UIScreenType.Ingame, false);

        DOVirtual.DelayedCall(2, () => ToggleScreen(UIScreenType.LoseScreen, true));
    }

    protected void OnShowWinScreen()
    {
        DOVirtual.DelayedCall(5, () => ToggleScreen(UIScreenType.WinScreen, true));
    }
}
