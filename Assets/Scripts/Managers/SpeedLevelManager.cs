//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class SpeedLevelManager : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private float speed = 1;
    [SerializeField] private float MaxSpeed = 1;

    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    private float startSpeed;

    //#endregion


    //#region life-cycle callbacks

    void Start()
    {
        startSpeed = speed;
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

    //#endregion

    //#region private methods

    private void SetLevelSpeed(float speed)
    {
        float newSpeed = MaxSpeed * speed;



        if (newSpeed > MaxSpeed)
        {
            this.speed = MaxSpeed;
        }
        else
        {
            this.speed = newSpeed + startSpeed;
        }

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
