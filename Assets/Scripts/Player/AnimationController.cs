//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class AnimationController : MonoBehaviour
{
    //#region editors fields and properties

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRender;

    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    private MovingController movingController;

    private readonly int speedAnimator = Animator.StringToHash("speed");
    private readonly int jumpAnimator = Animator.StringToHash("jump");
    private readonly int isGroundedAnimator = Animator.StringToHash("isGrounded");

    private bool _isRun = false;
    private bool _isJump = false;

    //#endregion


    //#region life-cycle callbacks
    void Awake()
    {
        movingController = GetComponent<MovingController>();
    }

    void FixedUpdate()
    {
        Run(movingController.GetCurrentSpeed());
    }

    //#endregion

    //#region public methods

    public void Run(float speed)
    {
        if (_isJump) return;
        animator.SetFloat(speedAnimator, speed);
    }

    public void Jump()
    {
        _isJump = true;
        animator.SetTrigger(jumpAnimator);
    }

    public void ChangeIsGrounded(bool isGrounded)
    {
        animator.SetBool(isGroundedAnimator, isGrounded);

        if (isGrounded)
        {
            _isJump = false;
        }
    }

    public void FlipX(Vector2 direction)
    {
        if (direction.x > 0)
        {
            spriteRender.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRender.flipX = true;
        }
    }

    //#endregion

    //#region private methods

    //#endregion

    //#region event handlers
    //#endregion
}
