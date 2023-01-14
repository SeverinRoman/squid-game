//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class EnemyCharactrer : BaseCharacter
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

    private void ChangeGameSpeed()
    {

    }

    //#endregion

    //#region event handlers

    protected void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case ((int)LayerType.Player):
                ChangeGameSpeed();
                State = CharacterState.Death;
                break;
        }


    }

    //#endregion
}
