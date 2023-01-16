//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//#endregion


public class PlayerCharacter : BaseCharacter
{
    //#region editors fields and properties
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TweenConfig tweenPrepere;

    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    private Rigidbody rigidbody;

    //#endregion


    //#region life-cycle callbacks

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        GameEventManager.PrepereLevel.AddListener(OnPrepereLevel);
    }

    void OnDisable()
    {
        GameEventManager.PrepereLevel.RemoveListener(OnPrepereLevel);
    }

    //#endregion

    //#region public methods
    public override void Death()
    {
        GameEventManager.SetCameraFollow?.Invoke(CameraType.MainCamera, null);
        base.Death();
        GameEventManager.PlayerDeath?.Invoke();

    }

    public void PushEnemy()
    {
        State = CharacterState.ClimbStop;
    }
    //#endregion

    //#region private methods

    private void PrepereLevel()
    {
        rigidbody.isKinematic = true;
        playerController.ToggleInput(false);
        tweenPrepere.tween = transform.DOLocalMove(tweenPrepere.transformPosition, tweenPrepere.duration);
        tweenPrepere.SetSettingsTween();
    }

    //#endregion

    //#region event handlers

    protected void OnPrepereLevel()
    {
        PrepereLevel();
    }

    protected void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        switch (gameObject.layer)
        {
            case ((int)LayerType.Obstacle):
                Death();
                break;
        }
    }

    //#endregion
}
