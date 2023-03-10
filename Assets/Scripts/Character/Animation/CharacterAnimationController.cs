//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
//#endregion


public class CharacterAnimationController : AnimationController
{
    //#region editors fields and properties

    [SerializeField] private BaseCharacter character;
    [SerializeField] private float speedClimb = 1;

    [SerializeField] private float randomDelayClimb = 1;

    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties
    //#endregion


    //#region life-cycle callbacks

    void Start()
    {
        SetCurrentSpeed();
    }

    void OnEnable()
    {
        GameEventManager.ChangeLevelSpeed.AddListener(OnChangeLevelSpeed);
    }

    void OnDisable()
    {
        GameEventManager.ChangeLevelSpeed.RemoveListener(OnChangeLevelSpeed);
    }
    //#endregion

    //#region public methods

    public void ClimbStop()
    {
        animator.SetTrigger(animations.character.climbStop);
    }

    public void Climb(bool isClimb)
    {
        DOVirtual.DelayedCall(UnityEngine.Random.Range(0, randomDelayClimb), () => animator.SetBool(animations.character.isClimb, isClimb));
    }

    public void Jump()
    {
        animator.SetTrigger(animations.character.jump);
    }

    //#endregion

    //#region private methods

    private void SetCurrentSpeed()
    {
        float speed = 1;
        Action<float> callback = (value) =>
        {
            speed = value;
        };
        GameEventManager.GetLevelSpeed?.Invoke(callback);

        animator.speed = speedClimb * speed;
    }

    private void JumpEnd()
    {
        character.State = CharacterState.Climb;
    }

    //#endregion

    //#region event handlers

    protected void OnJumpEnd()
    {
        JumpEnd();
    }

    protected void OnChangeLevelSpeed()
    {
        SetCurrentSpeed();
    }

    //#endregion
}
