//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//#endregion


public class CharacterAnimationController : AnimationController
{
    //#region editors fields and properties

    [SerializeField] private BaseCharacter character;
    [SerializeField] private float speedClimb = 1;

    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    //#endregion


    //#region life-cycle callbacks
    void Awake()
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

    public void Climb(bool isClimb)
    {
        animator.SetBool(animations.character.isClimb, isClimb);
        animator.speed = speedClimb;
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

        speedClimb *= speed;

        animator.speed = speedClimb;
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
