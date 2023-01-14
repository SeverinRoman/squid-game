//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//#endregion


public class ButtonBase : MonoBehaviour
{
    //#region editors fields and properties
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    private Button button;

    //#endregion


    //#region life-cycle callbacks

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods
    //#endregion

    //#region event handlers

    virtual protected void onClick()
    {
        // GameEventManager.ChangeNextLevel?.Invoke(0);
    }

    //#endregion
}
