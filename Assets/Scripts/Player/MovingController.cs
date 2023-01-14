//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class MovingController : MonoBehaviour
{
    //#region editors fields and properties

    public float speedWalk = 1;
    public float speedRun = 1;
    public float forceJump = 1;

    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    private CollisionController _collisionController;
    private Rigidbody2D _rigidbody;

    private AnimationController _animationController;

    private bool isRun = false;

    //#endregion


    //#region life-cycle callbacks

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionController = GetComponent<CollisionController>();
        _animationController = GetComponent<AnimationController>();
    }

    private void FixedUpdate()
    {
        CheackChangeWalk();
    }

    //#endregion

    //#region public methods

    public void MoveX(Vector2 direction)
    {
        if (direction == null || Vector2.zero == direction) return;

        _animationController.FlipX(direction);


        float speed = speedWalk;
        if (isRun)
        {
            speed = speedRun;
        }

        _rigidbody.velocity = new Vector2(direction.x * speed, _rigidbody.velocity.y);
    }

    public void CheackChangeWalk()
    {
        if (GetCurrentSpeed() <= 0f)
        {
            isRun = false;
        }
    }

    public void Run()
    {
        this.isRun = true;
    }

    public float GetCurrentSpeed()
    {
        return _rigidbody.velocity.magnitude;
    }

    public void Jump()
    {
        if (!_collisionController.GetIsGrounded()) return;

        _animationController.Jump();
        _rigidbody.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
    }

    //#endregion

    //#region private methods

    //#endregion

    //#region event handlers
    //#endregion
}
