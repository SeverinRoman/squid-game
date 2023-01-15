//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//#endregion


public class Stamina : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private Image progress;
    [SerializeField] private float startProgress;
    [SerializeField] private float stepChangeStamina;
    [SerializeField] private float stepIncreaseStamina;
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties
    //#endregion


    //#region life-cycle callbacks

    void Start()
    {
        progress.fillAmount = startProgress;
    }

    void OnEnable()
    {
        GameEventManager.ChangeStamina.AddListener(OnChangeStamina);
    }

    void OnDisable()
    {
        GameEventManager.ChangeStamina.RemoveListener(OnChangeStamina);
    }

    void Update()
    {
        IncreaseStamina();
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private void ChangeStamina()
    {
        progress.fillAmount += stepChangeStamina;

        GameEventManager.SetLevelSpeed?.Invoke(progress.fillAmount);
    }

    private void IncreaseStamina()
    {
        if (progress.fillAmount <= 0) return;
        float step = stepIncreaseStamina + progress.fillAmount * 0.05f;

        progress.fillAmount -= (step * Time.deltaTime);

        GameEventManager.SetLevelSpeed?.Invoke(progress.fillAmount);
    }

    //#endregion

    //#region event handlers

    protected void OnChangeStamina()
    {
        ChangeStamina();
    }

    //#endregion
}
