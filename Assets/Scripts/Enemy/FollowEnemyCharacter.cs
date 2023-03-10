//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//#endregion


public class FollowEnemyCharacter : EnemyCharactrer
{
    //#region editors fields and properties
    [SerializeField] private float speedMovingUp;
    [SerializeField] private float randomDelayMoving;
    [SerializeField] private TweenConfig tweenMoving;

    [SerializeField] private TweenConfig tweenMovingUp;
    [SerializeField] private TweenConfig tweenMovingDown;
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    private bool isFirstKill = false;

    //#endregion


    //#region life-cycle callbacks

    void Awake()
    {
        RandomeMoving();
    }
    void OnEnable()
    {
        GameEventManager.PlayerDeath.AddListener(OnPlayerDeath);
        GameEventManager.PrepereLevel.AddListener(OnPrepereLevel);
        GameEventManager.KillFirstEnemy.AddListener(OnKillFirstEnemy);
    }

    void OnDisable()
    {
        GameEventManager.PlayerDeath.RemoveListener(OnPlayerDeath);
        GameEventManager.PrepereLevel.RemoveListener(OnPrepereLevel);
        GameEventManager.KillFirstEnemy.RemoveListener(OnKillFirstEnemy);
    }
    //#endregion

    //#region public methods
    public override void CollidePlayer(GameObject player)
    {
        BaseCharacter playerCharacter = player.GetComponent<BaseCharacter>();
        playerCharacter.State = CharacterState.Death;
    }

    public override void CollideEnemy(GameObject enemy)
    {

    }
    //#endregion

    //#region private methods

    private void RandomeMoving()
    {
        tweenMoving.tween = transform.DOLocalMove(tweenMoving.transformPosition + transform.localPosition, tweenMoving.duration + UnityEngine.Random.Range(0, randomDelayMoving));
        tweenMoving.SetSettingsTween();

        tweenMoving.tween.OnStepComplete(() =>
        {
            CheackSpeed();
        });
    }

    private void CheackSpeed()
    {
        float speed = 1;
        Action<float> callback = (value) =>
        {
            speed = value;
        };
        GameEventManager.GetLevelSpeed?.Invoke(callback);
        if (speed <= speedMovingUp)
        {
            MovingUp();
        }
    }

    private void MovingUp()
    {
        if (!isFirstKill) return;

        tweenMoving.tween.Kill();

        tweenMovingUp.tween = transform.DOLocalMove(tweenMovingUp.transformPosition + transform.localPosition, tweenMovingUp.duration + UnityEngine.Random.Range(0, randomDelayMoving));
        tweenMovingUp.SetSettingsTween();

        tweenMovingUp.tween.OnStepComplete(() =>
        {
            tweenMovingUp.tween.Kill();
            Destroy(gameObject);
        });
    }

    private void MovingDown()
    {
        if (!isFirstKill) return;

        tweenMoving.tween.Kill();

        tweenMovingDown.tween = transform.DOLocalMove(tweenMovingDown.transformPosition + transform.localPosition, tweenMovingDown.duration + UnityEngine.Random.Range(0, randomDelayMoving));
        tweenMovingDown.SetSettingsTween();

        tweenMovingDown.tween.OnComplete(() =>
        {
            tweenMovingDown.tween.Kill();
            Destroy(gameObject);
        });
    }


    //#endregion

    //#region event handlers

    protected void OnKillFirstEnemy()
    {
        isFirstKill = true;
        GameEventManager.KillFirstEnemy.RemoveListener(OnKillFirstEnemy);
    }

    protected void OnPrepereLevel()
    {
        MovingDown();
    }

    protected void OnPlayerDeath()
    {
        if (tweenMovingUp.tween == null)
        {
            MovingUp();
        }
    }

    public override void OnTriggerEnter(Collider other)
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
