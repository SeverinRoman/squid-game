
public enum GameEventType
{
    None = 0,

    //LevelManager

    ChangeLevel = 100,
    ChangeNextLevel = 101,
    RestartLevel = 102,
    //UI

    ToggleScreen = 200,

    //EffectManager
    SpawnEffect = 300,

    //AnnouncerManager
    SpawnAnnounce = 400,

    //Camera
    CameraFollowToggle = 500,

    //PoolManager
    GetPoolObject = 600,
    ReturnPoolObject = 601,

    //SaveLoadManager
    ChangeDataSave = 700,
    GetDataSave = 701,
    LoadDataSaveComplete = 702,


    //LoadManager
    LoadScene = 800,
}
