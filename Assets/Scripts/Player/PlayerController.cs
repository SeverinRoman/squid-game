//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class PlayerController : MonoBehaviour
{
    //#region editors fields and properties

    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    private MovingController _movingController;

    private bool isRun = false;

    //#endregion


    //#region life-cycle callbacks

    void Awake()
    {
        _movingController = GetComponent<MovingController>();
    }


    void OnEnable()
    {
        GameEventManager.InputAxis.AddListener(OnInputAxis);
        GameEventManager.InputButton.AddListener(OnInputButton);
    }

    void OnDisable()
    {
        GameEventManager.InputAxis.RemoveListener(OnInputAxis);
        GameEventManager.InputButton.RemoveListener(OnInputButton);
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private void Move(Vector2 input)
    {
        _movingController.MoveX(input);
    }

    private void Jump()
    {
        _movingController.Jump();
    }

    private void Run()
    {
        _movingController.Run();
    }

    //#endregion

    //#region event handlers

    protected void OnInputAxis(Vector2 input)
    {
        Move(input);
    }

    protected void OnInputButton(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.Jump:
                {
                    Jump();
                }
                break;
            case ButtonType.Run:
                {
                    Run();
                }
                break;
            default:
                Debug.Log("Unknown button");
                break;
        }

    }

    //#endregion
}
