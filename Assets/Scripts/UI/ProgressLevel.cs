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

        GameEventManager.PlayerDeath.AddListener(OnPlayerDeath);
        GameEventManager.KillFirstEnemy.AddListener(OnKillFirstEnemy);
    }

    void OnDisable()
    {
        GameEventManager.PlayerDeath.RemoveListener(OnPlayerDeath);
        GameEventManager.KillFirstEnemy.RemoveListener(OnKillFirstEnemy);
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods
    private void IncreaseStart()
    {
        tweenProgress = DOTween.To(() => progress.fillAmount, v => progress.fillAmount = v, 1f, durationLevel).SetEase(Ease.Linear);

        tweenProgress.OnComplete(() =>
        {
            LevelComplete();
        });
    }

    private void LevelComplete()
    {
        GameEventManager.PrepereLevel?.Invoke();
        GameEventManager.ToggleScreen?.Invoke(UIScreenType.Ingame, false);
    }

    //#endregion

    //#region event handlers

    protected void OnKillFirstEnemy()
    {
        IncreaseStart();
        GameEventManager.KillFirstEnemy.RemoveListener(OnKillFirstEnemy);
    }

    protected void OnPlayerDeath()
    {
        tweenProgress.Pause();
    }

    //#endregion
}
