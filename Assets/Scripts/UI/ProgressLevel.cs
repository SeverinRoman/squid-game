//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//#endregion


public class ProgressLevel : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private float startProgress;
    [SerializeField] private float durationLevel;
    [SerializeField] private Image progress;
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    private Tween tweenProgress;

    //#endregion


    //#region life-cycle callbacks
    void Start()
    {
        progress.fillAmount = startProgress;
    }
    void OnEnable()
    {
        GameEventManager.KillFirstEnemy.AddListener(OnKillFirstEnemy);
    }

    void OnDisable()
    {
        GameEventManager.KillFirstEnemy.RemoveListener(OnKillFirstEnemy);
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods
    private void IncreaseStart()
    {
        tweenProgress = DOTween.To(() => progress.fillAmount, v => progress.fillAmount = v, 1f, durationLevel);

        tweenProgress.OnComplete(() =>
        {
            LevelComplete();
        });
    }

    private void LevelComplete()
    {
        Debug.Log("Level Complete");
    }

    //#endregion

    //#region event handlers

    protected void OnKillFirstEnemy()
    {
        IncreaseStart();
        GameEventManager.KillFirstEnemy.RemoveListener(OnKillFirstEnemy);
    }

    //#endregion
}
