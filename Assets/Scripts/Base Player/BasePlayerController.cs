using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasePlayerController : MonoBehaviour
{
    private BasePlayerModel _model;
    private PlayerInputActions _input;

    private void Awake()
    {
        _model = GetComponent<BasePlayerModel>();
        
        _input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.BasePlayer.Movement.performed += MovementInput;
        _input.BasePlayer.Movement.canceled += MovementCancel;
    }

    private void OnDisable()
    {
        _input.Disable();
        
        _input.BasePlayer.Movement.performed -= MovementInput;
        _input.BasePlayer.Movement.canceled -= MovementCancel;
    }

    private void MovementInput(InputAction.CallbackContext value)
    {
        _model.InputDir = value.ReadValue<Vector2>();
    }

    private void MovementCancel(InputAction.CallbackContext value)
    {
        _model.InputDir = Vector2.zero;
    }
}
