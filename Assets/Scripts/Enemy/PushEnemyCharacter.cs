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
    [SerializeField] private float cheacksBodyDown = 5;
    [SerializeField] private Transform cameraFollow;
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties
    private bool isCheackOnlyUp = false;
    private float previousY = 0;

    private float currentCheacksBodyDown = 0;
    //#endregion


    //#region life-cycle callbacks

    // void Update()
    // {
    //     if (isCheackOnlyUp)
    //     {
    //         CheackOnlyUp();
    //     }
    // }

    void FixedUpdate()
    {
        if (isCheackOnlyUp)
        {
            CheackOnlyUp();
        }
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
        GameEventManager.CameraToggle?.Invoke(CameraType.MainCamera, false);
        GameEventManager.ShowWinScreen?.Invoke();
        GameEventManager.ToggleScreen?.Invoke(UIScreenType.ScoreScreen, true);
        GameEventManager.SetCameraFollow?.Invoke(CameraType.PushEnemyCamera, cameraFollow);

        isCheackOnlyUp = true;

        PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();

        playerCharacter.PushEnemy();

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
            currentCheacksBodyDown += 1;
        }
        else
        {
            GameEventManager.ScoreIncrease?.Invoke();
            GameEventManager.SetCameraFollow?.Invoke(CameraType.PushEnemyCamera, cameraFollow);
            previousY = cameraFollow.position.y;
        }

        if (currentCheacksBodyDown >= cheacksBodyDown)
        {

            isCheackOnlyUp = false;
            GameEventManager.SetCameraFollow?.Invoke(CameraType.PushEnemyCamera, null);
        }
    }

    //#endregion

    //#region event handlers
    //#endregion
}
