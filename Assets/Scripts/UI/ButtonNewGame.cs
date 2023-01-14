//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class ButtonNewGame : ButtonBase
{
    //#region editors fields and properties
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties
    //#endregion


    //#region life-cycle callbacks
    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private void ChangeLevel()
    {
        GameEventManager.ChangeLevel?.Invoke(LevelType.Level1, 0);
    }

    //#endregion

    //#region event handlers

    protected override void onClick()
    {
        ChangeLevel();
    }

    //#endregion
}
