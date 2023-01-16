//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//#endregion


public class Settingss : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private TMP_Text text;
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    private bool setSixteenFPS = false;

    //#endregion


    //#region life-cycle callbacks
    //#endregion

    //#region public methods

    public void ChangeFPS()
    {
        if (setSixteenFPS)
        {
            Application.targetFrameRate = 30;
            text.text = "30";
        }
        else
        {
            Application.targetFrameRate = 60;
            text.text = "60";
        }
        setSixteenFPS = !setSixteenFPS;
    }

    //#endregion

    //#region private methods
    //#endregion

    //#region event handlers
    //#endregion
}
