//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion

[System.Serializable]
public class SaveData
{
    [SerializeField] public LevelType CurrentLevel = LevelType.Level1;
}