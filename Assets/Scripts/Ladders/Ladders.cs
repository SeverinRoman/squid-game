//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//#endregion


public class Ladders : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private float withSpawn;
    [SerializeField] private GameObject holderLadders;

    [Header("Tweens")]
    [SerializeField] private TweenConfig tweenMove;
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties
    private float speedLadder;
    //#endregion


    //#region life-cycle callbacks

    void Awake()
    {
        MoveLadders();
        StopMove();
    }
    void Start()
    {
        SetCurrentSpeed();
        StartMove();
    }

    void OnEnable()
    {
        GameEventManager.PlayerDeath.AddListener(OnPlayerDeath);
        GameEventManager.ChangeLevelSpeed.AddListener(OnChangeLevelSpeed);
    }

    void OnDisable()
    {
        GameEventManager.PlayerDeath.AddListener(OnPlayerDeath);
        GameEventManager.ChangeLevelSpeed.RemoveListener(OnChangeLevelSpeed);
    }

    //#endregion

    //#region public methods

    public void StartMove()
    {
        tweenMove.tween.Play();
    }

    public void StopMove()
    {
        tweenMove.tween.Pause();
    }


    //#endregion

    //#region private methods

    private void SetCurrentSpeed()
    {
        float speed = 1;
        Action<float> callback = (value) =>
        {
            speed = value;
        };
        GameEventManager.GetLevelSpeed?.Invoke(callback);



        tweenMove.tween.timeScale = speed;
    }

    private void MoveLadders()
    {
        tweenMove.tween = holderLadders.transform.DOLocalMove(tweenMove.transformPosition, tweenMove.duration);
        tweenMove.tween.SetEase(tweenMove.easing);
        tweenMove.tween.SetLoops(tweenMove.loops, tweenMove.loopType);
    }

    //#endregion

    //#region event handlers

    protected void OnChangeLevelSpeed()
    {
        SetCurrentSpeed();
    }

    protected void OnPlayerDeath()
    {
        tweenMove.tween.Pause();
    }





    //#endregion
}
