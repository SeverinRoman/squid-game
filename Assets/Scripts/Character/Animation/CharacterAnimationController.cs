//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//#endregion


public class CharacterAnimationController : AnimationController
{
    //#region editors fields and properties

    [SerializeField] private BaseCharacter character;

    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties
    //#endregion


    //#region life-cycle callbacks
    //#endregion

    //#region public methods

    public void Climb(bool isClimb)
    {
        animator.SetBool(animations.character.isClimb, isClimb);
    }

    public void Jump()
    {
        animator.SetTrigger(animations.character.jump);
    }

    //#endregion

    //#region private methods

    private void JumpEnd()
    {
        character.State = CharacterState.Idle;
    }

    //#endregion

    //#region event handlers

    protected void OnJumpEnd()
    {
        JumpEnd();
    }

    //#endregion
}
