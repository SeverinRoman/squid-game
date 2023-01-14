//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//#endregion


public class SaveLoadManager : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private SaveData saveData;
    [SerializeField] private bool deleteSave = false;
    [SerializeField] private string fileName = "Save";
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties



    //#endregion


    //#region life-cycle callbacks
    void Start()
    {
        if (deleteSave)
        {
            DeleteSave();
        }
        Init();
    }

    void OnDisable()
    {
        GameEventManager.ChangeDataSave.AddListener(OnChangeDataSave);
        GameEventManager.GetDataSave.AddListener(OnGetDataSave);
    }

    void OnEnable()
    {
        GameEventManager.ChangeDataSave.RemoveListener(OnChangeDataSave);
        GameEventManager.GetDataSave.RemoveListener(OnGetDataSave);
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private void Init()
    {
        if (!JsonSaver.IsExistsSave(fileName))
        {
            var json = new JsonValueGeneric<SaveData>(saveData);
            JsonSaver.Save(json, fileName);
        }
        else
        {
            saveData = Load();
            GameEventManager.LoadDataSaveComplete?.Invoke();
        }
    }

    private void Save(SaveData newSaveData)
    {
        var json = new JsonValueGeneric<SaveData>(newSaveData);
        JsonSaver.Save(json, fileName);
    }

    private SaveData Load()
    {
        var json = JsonSaver.Load<JsonValueGeneric<SaveData>>(fileName);
        return json.Value;
    }

    private void DeleteSave()
    {
        JsonSaver.DeleteSave(fileName);
    }

    //#endregion

    //#region event handlers

    protected void OnChangeDataSave(SaveData newSaveData)
    {
        Save(newSaveData);
        saveData = newSaveData;
    }

    protected void OnGetDataSave(Action<SaveData> callback)
    {
        SaveData saveData = Load();
        callback(saveData);
    }


    //#endregion
}
