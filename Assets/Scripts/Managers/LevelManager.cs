using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;
using NaughtyAttributes;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    private class LevelConfig
    {
        [SerializeField] public LevelType type;
        [Scene]
        [SerializeField] public string sceneName;
    }

    [SerializeField] private List<LevelConfig> levelConfigs;

    private Tween timeChangeLevel = null;
    private Tween timeRestartLevel = null;

    private LevelConfig currentLevel;

    void Awake()
    {
        foreach (LevelConfig levelConfig in levelConfigs)
        {
            if (levelConfig.sceneName == SceneManager.GetActiveScene().name)
            {
                currentLevel = levelConfig;
            }
        }
    }
    void OnEnable()
    {
        GameEventManager.ChangeLevel.AddListener(OnChangeLevel);
        GameEventManager.ChangeNextLevel.AddListener(OnChangeNextLevel);
        GameEventManager.RestartLevel.AddListener(OnRestartLevel);
    }

    void OnDisable()
    {
        GameEventManager.ChangeLevel.RemoveListener(OnChangeLevel);
        GameEventManager.ChangeNextLevel.RemoveListener(OnChangeNextLevel);
        GameEventManager.RestartLevel.RemoveListener(OnRestartLevel);
    }

    private void ChangeLevel(LevelType type)
    {
        foreach (LevelConfig levelConfig in levelConfigs)
        {
            if (levelConfig.type == type)
            {
                GameEventManager.LoadScene?.Invoke(SceneManager.GetSceneByName(levelConfig.sceneName).name);
                // SceneManager.LoadScene(levelConfig.sceneName);
            }
        }
    }

    public void RestartLevel()
    {
        DOTween.KillAll();
        GameEventManager.LoadScene?.Invoke(SceneManager.GetActiveScene().name);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void CheakTwinsTime()
    {
        if (timeRestartLevel != null || timeChangeLevel != null)
        {
            timeChangeLevel.Kill();
        }
    }

    private void ChangeNextLevel()
    {
        for (int i = 0; i < levelConfigs.Count; i++)
        {
            if (levelConfigs[i].type == currentLevel.type)
            {
                if (i + 1 >= levelConfigs.Count)
                {
                    ChangeLevel(levelConfigs[0].type);
                }
                else
                {
                    ChangeLevel(levelConfigs[i + 1].type);
                }

            }
        }
    }

    protected void OnChangeLevel(LevelType type, float timeToChange = 0f)
    {
        CheakTwinsTime();
        if (timeToChange == 0)
        {
            ChangeLevel(type);
        }
        else
        {
            float time = 0f;
            timeChangeLevel = DOTween.To(() => time, x => time = x, timeToChange, timeToChange);
            timeChangeLevel.OnComplete(() =>
            {
                ChangeLevel(type);
            });
        }
    }

    protected void OnRestartLevel(float timeToRestart = 0f)
    {
        CheakTwinsTime();
        if (timeToRestart == 0)
        {
            RestartLevel();
        }
        else
        {
            float time = 0f;
            timeChangeLevel = DOTween.To(() => time, x => time = x, timeToRestart, timeToRestart);
            timeChangeLevel.OnComplete(() =>
            {
                RestartLevel();
            });
        }
    }

    protected void OnChangeNextLevel(float timeToChange = 0f)
    {
        CheakTwinsTime();
        if (timeToChange == 0)
        {
            ChangeNextLevel();
        }
        else
        {
            float time = 0f;
            timeChangeLevel = DOTween.To(() => time, x => time = x, timeToChange, timeToChange);
            timeChangeLevel.OnComplete(() =>
            {
                ChangeNextLevel();
            });
        }
    }
}
