//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class SpeedLevelManager : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField]
    public float speed = 1;
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    //#endregion


    //#region life-cycle callbacks

    void Start()
    {
        GameEventManager.ChangeLevelSpeed?.Invoke();
    }

    void OnEnable()
    {
        GameEventManager.SetLevelSpeed.AddListener(OnSetLevelSpeed);
        GameEventManager.GetLevelSpeed.AddListener(OnGetLevelSpeed);
    }

    void OnDisable()
    {
        GameEventManager.SetLevelSpeed.RemoveListener(OnSetLevelSpeed);
        GameEventManager.GetLevelSpeed.RemoveListener(OnGetLevelSpeed);
    }

    //#endregion

    //#region public methods
    [NaughtyAttributes.Button]
    public void UpSpeed()
    {
        SetLevelSpeed(this.speed += 1f);
    }

    //#endregion

    //#region private methods

    private void SetLevelSpeed(float speed)
    {
        this.speed = speed;

        GameEventManager.ChangeLevelSpeed?.Invoke();
    }

    //#endregion

    //#region event handlers

    protected void OnSetLevelSpeed(float speed)
    {
        SetLevelSpeed(speed);
    }

    protected void OnGetLevelSpeed(Action<float> callback)
    {
        callback(speed);
    }

    //#endregion
}
