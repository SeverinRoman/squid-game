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
    [SerializeField] private float stepDecreaseStamina;
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    private bool isDecrease = true;

    //#endregion


    //#region life-cycle callbacks

    void Start()
    {
        progress.fillAmount = startProgress;
    }

    void OnEnable()
    {
        GameEventManager.PrepereLevel.AddListener(OnPrepereLevel);
        GameEventManager.ChangeStamina.AddListener(OnChangeStamina);
    }

    void OnDisable()
    {
        GameEventManager.PrepereLevel.RemoveListener(OnPrepereLevel);
        GameEventManager.ChangeStamina.RemoveListener(OnChangeStamina);
    }

    void Update()
    {
        if (isDecrease)
        {
            DecreaseStamina();
        }
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private void ChangeStamina()
    {
        progress.fillAmount += stepChangeStamina;

        GameEventManager.SetLevelSpeed?.Invoke(progress.fillAmount);

        CheackColorProgress();
    }

    private void DecreaseStamina()
    {
        if (progress.fillAmount <= 0) return;
        float step = stepDecreaseStamina + progress.fillAmount * 0.1f;

        progress.fillAmount -= (step * Time.deltaTime);

        GameEventManager.SetLevelSpeed?.Invoke(progress.fillAmount);

        CheackColorProgress();
    }

    private void CheackColorProgress()
    {
        if (progress.fillAmount < 0.3f)
        {
            progress.color = Color.red;
        }
        else if (progress.fillAmount < 0.6f)
        {
            progress.color = Color.magenta;
        }
        else if (progress.fillAmount < 0.8f)
        {
            progress.color = Color.green;
        }
    }

    //#endregion

    //#region event handlers

    protected void OnChangeStamina()
    {
        ChangeStamina();
    }

    protected void OnPrepereLevel()
    {
        isDecrease = false;
    }

    //#endregion
}
