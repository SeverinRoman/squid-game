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

    public virtual void CollidePlayer(GameObject player)
    {
        GameEventManager.KillFirstEnemy?.Invoke();
        State = CharacterState.Death;


        GameEventManager.ChangeStamina?.Invoke();
    }

    public virtual void CollideEnemy(GameObject enemy)
    {
        State = CharacterState.Death;
    }

    //#endregion

    //#region private methods



    //#endregion

    //#region event handlers

    protected void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        switch (gameObject.layer)
        {
            case ((int)LayerType.Player):
                CollidePlayer(gameObject);
                break;
            case ((int)LayerType.Enemy):
                CollideEnemy(gameObject);
                break;
        }
    }

    //#endregion
}
