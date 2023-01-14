//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class CollisionController : MonoBehaviour
{
    //#region editors fields and properties
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    private AnimationController _animationController;
    private bool _isGround = false;

    //#endregion


    //#region life-cycle callbacks

    void Awake()
    {
        _animationController = GetComponent<AnimationController>();
    }

    //#endregion

    //#region public methods

    public bool GetIsGrounded()
    {
        return _isGround;
    }


    //#endregion

    //#region private methods



    //#endregion

    //#region event handlers

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;

        switch (otherGameObject.layer)
        {
            case ((int)LayerType.Ground):
                _isGround = true;
                _animationController.ChangeIsGrounded(_isGround);

                break;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;

        switch (otherGameObject.layer)
        {
            case ((int)LayerType.Ground):
                _isGround = false;
                _animationController.ChangeIsGrounded(_isGround);
                break;
        }

    }

    //#endregion
}
