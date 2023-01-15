//#region import
using System;
using UnityEngine.Events;
using UnityEngine;
//#endregion


public static class GameEventManager
{
    //SaveLoadManager
    public static UnityEvent<SaveData> ChangeDataSave = new();
    public static UnityEvent<Action<SaveData>> GetDataSave = new();
    public static UnityEvent LoadDataSaveComplete = new();

    //PoolManager
    public static UnityEvent<GameObjectType, Action<GameObject>> GetPoolObject = new();
    public static UnityEvent<GameObjectType, GameObject> ReturnPoolObject = new();

    //LevelManager
    public static UnityEvent<LevelType, float> ChangeLevel = new();
    public static UnityEvent<float> ChangeNextLevel = new();
    public static UnityEvent<float> RestartLevel = new();

    //LoadSceneManager
    public static UnityEvent<string> LoadScene = new();

    //EffectManager
    public static UnityEvent<EffectType, Vector3> SpawnEffect = new();

    //UI
    public static UnityEvent<UIScreenType, bool> ToggleScreen = new();

    //InputManager
    public static UnityEvent<Vector2> InputAxis = new();
    public static UnityEvent<ButtonType> InputButton = new();

    //LoadScreen
    public static UnityEvent<float> ChangeProgressValue = new();

    //LevelSpeedManager
    public static UnityEvent<float> SetLevelSpeed = new();
    public static UnityEvent<Action<float>> GetLevelSpeed = new();
    public static UnityEvent ChangeLevelSpeed = new();


    //Enemy
    public static UnityEvent KillFirstEnemy = new();

    //Stamina
    public static UnityEvent ChangeStamina = new();

    //PlayerCharacter
    public static UnityEvent PlayerDeath = new();


}
