using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    private FirstPersonModel _model;

    private PlayerInputActions _input;

    private void Awake()
    {
        _model = GetComponent<FirstPersonModel>();
        
        _input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.FirstPersonPlayer.Jump.performed += JumpAction;
        _input.FirstPersonPlayer.Movement.performed += MoveAction;
        _input.FirstPersonPlayer.Movement.canceled += MoveCancel;
        _input.FirstPersonPlayer.Rotation.performed += RotationAction;
        _input.FirstPersonPlayer.Rotation.canceled += RotationCancel;
    }

    private void OnDisable()
    {
        _input.Disable();
        
        _input.FirstPersonPlayer.Jump.performed -= JumpAction;
        _input.FirstPersonPlayer.Movement.performed -= MoveAction;
        _input.FirstPersonPlayer.Movement.canceled -= MoveCancel;
        _input.FirstPersonPlayer.Rotation.performed -= RotationAction;
        _input.FirstPersonPlayer.Rotation.canceled -= RotationCancel;
    }

    private void JumpAction(InputAction.CallbackContext value)
    {
        _model.Jump();
    }

    private void MoveAction(InputAction.CallbackContext value)
    {
        _model.RawInput = value.ReadValue<Vector2>();
    }

    private void MoveCancel(InputAction.CallbackContext value)
    {
        _model.RawInput = Vector2.zero;
    }

    private void RotationAction(InputAction.CallbackContext value)
    {
        _model.MouseInput = value.ReadValue<Vector2>();
    }

    private void RotationCancel(InputAction.CallbackContext value)
    {
        _model.MouseInput = Vector2.zero;
    }
}
