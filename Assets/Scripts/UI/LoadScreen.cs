//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//#endregion


public class LoadScreen : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private List<GameObject> loadingScreen;
    [SerializeField] private Image loadingBarScreen;
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties
    //#endregion


    //#region life-cycle callbacks

    void Start()
    {
        ShowLoadingImage();
    }

    void OnEnable()
    {
        GameEventManager.ChangeProgressValue.AddListener(OnChangeProgressValue);
    }

    void OnDisable()
    {
        GameEventManager.ChangeProgressValue.RemoveListener(OnChangeProgressValue);
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private void ChangeProgressValue(float progressValue)
    {
        loadingBarScreen.fillAmount = progressValue;
    }

    private void ShowLoadingImage()
    {
        if (loadingScreen.Count > 0)
        {
            loadingScreen[Random.Range(0, loadingScreen.Count)].SetActive(true);
        }
    }
    //#endregion

    //#region event handlers

    protected void OnChangeProgressValue(float progressValue)
    {
        ChangeProgressValue(progressValue);
    }

    //#endregion
}
