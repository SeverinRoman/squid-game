//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class InputManager : MonoBehaviour
{
    //#region editors fields and properties

    [SerializeField] private FloatingJoystick floatingJoystick;

    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties
    //#endregion


    //#region life-cycle callbacks

    void Update()
    {
        CheackAxis();
        CheackButton();
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private void CheackAxis()
    {
        // GamepadAxis();
        KeybordAxis();
        JoystickAxis();
    }

    private void GamepadAxis()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        GameEventManager.InputAxis?.Invoke(new Vector2(horizontalInput, verticalInput));
    }
    private void JoystickAxis()
    {

        GameEventManager.InputAxis?.Invoke(new Vector2(floatingJoystick.Horizontal, floatingJoystick.Vertical));
    }

    private void KeybordAxis()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            input.x = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            input.x = 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            input.y = 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            input.y = -1;
        }

        if (input == Vector2.zero) return;

        GameEventManager.InputAxis?.Invoke(input);
    }

    private void CheackButton()
    {
        KeybordButtonsDown();
        KeybordButtonsUp();
    }

    private void KeybordButtonsDown()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameEventManager.InputButton?.Invoke(ButtonType.Jump);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GameEventManager.InputButton?.Invoke(ButtonType.Run);
        }
    }

    private void KeybordButtonsUp()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            GameEventManager.InputButton?.Invoke(ButtonType.RunEnd);
        }
    }

    //#endregion

    //#region event handlers
    //#endregion
}
