//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class PlayerCharacter : BaseCharacter
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
    public override void Death()
    {
        base.Death();
        GameEventManager.PlayerDeath?.Invoke();
    }
    //#endregion

    //#region private methods
    //#endregion

    //#region event handlers
    //#endregion
}
