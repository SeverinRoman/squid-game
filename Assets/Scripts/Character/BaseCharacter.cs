//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
//#endregion


public enum CharacterState
{
    Idle = 0,
    Climb = 1,
    Death = 3,
}

public class BaseCharacter : MonoBehaviour
{
    //#region editors fields and properties

    [SerializeField] private CharacterAnimationController characterAnimationController;

    //#endregion
    //#region public fields and properties

    public CharacterState State
    {
        get
        {
            return state;
        }
        set
        {
            if (state == value)
            {
                return;
            }

            ChangeState(value);
            state = value;
        }
    }

    //#endregion
    //#region private fields and properties

    private CharacterState state;

    //#endregion
    //#region life-cycle callbacks

    void Start()
    {
        State = CharacterState.Climb;
    }

    //#endregion

    //#region public methods
    public virtual void ChangeState(CharacterState state)
    {

        switch (state)
        {
            case CharacterState.Idle:
                {
                    Idle();
                }
                break;
            case CharacterState.Climb:
                {
                    Climb();
                }
                break;
            case CharacterState.Death:
                {
                    Death();
                }
                break;
        }
    }

    public virtual void Idle()
    {
        characterAnimationController.Climb(false);
        // Destroy(gameObject);
    }


    public virtual void Climb()
    {
        characterAnimationController.Climb(true);
        // Destroy(gameObject);
    }

    public virtual void Death()
    {
        characterAnimationController.Climb(false);
        // Destroy(gameObject);
    }





    //#endregion

    //#region private methods

    //#endregion

    //#region event handlers
    //#endregion
}

