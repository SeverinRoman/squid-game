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
    private Rigidbody _rigidbody;

    private AnimationController _animationController;

    private bool isClimb = false;

    //#endregion


    //#region life-cycle callbacks

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collisionController = GetComponent<CollisionController>();
        _animationController = GetComponent<AnimationController>();
    }

    //#endregion

    //#region public methods
    public void Climb()
    {
        this.isClimb = true;
    }

    public float GetCurrentSpeed()
    {
        return _rigidbody.velocity.magnitude;
    }

    public void Jump()
    {

    }

    //#endregion

    //#region private methods

    //#endregion

    //#region event handlers
    //#endregion
}
