using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Threading;

public class PlayerInputController : MonoBehaviour, InputControl.IPlayerActions
{
    private InputControl _ic;
    private Vector2 _movementP;

    public Vector2 MovementP { get { return _movementP; } }

    private void Awake()
    {
        _ic = new InputControl();
        _ic.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        _ic.Player.Enable();
    }

    private void OnDisable()
    {
        _ic.Player.Disable();
    }

    void InputControl.IPlayerActions.OnWalk(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _movementP = context.ReadValue<Vector2>();
            PlayerAnimationController.Instance.SetWalk(true);
        }
        else if (context.canceled)
        {
            _movementP = Vector2.zero;
            PlayerAnimationController.Instance.SetWalk(false);
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlayerAnimationController.Instance.SetRun(true);
        }
        else if (context.canceled)
        {
            PlayerAnimationController.Instance.SetRun(false);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlayerAnimationController.Instance.ActiveJump();
        }
    }

    public void OnChangeCamera(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            string activeCamera = CameraController.Instance.GetActiveCameraName();

            activeCamera = activeCamera == ConstantValue.ThirdPerson ? ConstantValue.FirstPerson : ConstantValue.ThirdPerson;

            CameraController.Instance.SetCameraByName(activeCamera);
        }
    }

    public void OnDance(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CameraController.Instance.SetTemporalCameraByName(ConstantValue.FrontPerson, 3f);
        }
    }
}