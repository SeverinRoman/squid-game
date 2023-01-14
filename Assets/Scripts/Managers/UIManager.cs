using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameEventManager.ToggleScreen.AddListener(OnToggleScreen);
    }

    private void OnDisable()
    {
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
}
