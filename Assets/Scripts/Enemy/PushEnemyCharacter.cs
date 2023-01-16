//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class PushEnemyCharacter : EnemyCharactrer
{
    //#region editors fields and properties
    [SerializeField] private float multiplayImpulse = 1;
    [SerializeField] private Transform cameraFollow;
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties
    private bool isCheackOnlyUp = false;
    private float previousY = 0;
    //#endregion


    //#region life-cycle callbacks

    void FixedUpdate()
    {
        CheackOnlyUp();
    }

    //#endregion

    //#region public methods
    public override void CollidePlayer(GameObject player)
    {
        State = CharacterState.Death;

        float speed = 1;
        Action<float> callback = (value) =>
        {
            speed = value;
        };
        GameEventManager.GetLevelSpeed?.Invoke(callback);

        ragdollController.Impulse(new Vector3(0f, speed * multiplayImpulse, 0f));

        GameEventManager.PushLastEnemy?.Invoke();
        GameEventManager.SetCameraFollow?.Invoke(CameraType.PushEnemyCamera, cameraFollow);
        GameEventManager.CameraToggle?.Invoke(CameraType.MainCamera, false);
        GameEventManager.ShowWinScreen?.Invoke();

        PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();

        playerCharacter.PushEnemy();

        isCheackOnlyUp = true;
    }

    public override void CollideEnemy(GameObject enemy)
    {
    }
    //#endregion

    //#region private methods

    private void CheackOnlyUp()
    {
        if (previousY > cameraFollow.position.y)
        {
            GameEventManager.SetCameraFollow?.Invoke(CameraType.PushEnemyCamera, null);
        }
        else
        {
            GameEventManager.SetCameraFollow?.Invoke(CameraType.PushEnemyCamera, cameraFollow);
            previousY = cameraFollow.position.y;
        }
    }

    //#endregion

    //#region event handlers
    //#endregion
}
