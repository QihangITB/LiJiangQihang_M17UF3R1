using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Threading;

public class PlayerInputController : MonoBehaviour, InputControl.IPlayerActions
{
    private InputControl _ic;
    private Vector2 _movementP;
    private bool isMoving = false;
    private bool isRunning = false;
    private bool isCrouching = false;

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

    private void FixedUpdate()
    {
        if(isMoving)
        {
            PlayerAnimationController.Instance.SetWalk();
        }

        if (isRunning)
        {
            PlayerAnimationController.Instance.SetRun();
        }
    }

    void InputControl.IPlayerActions.OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _movementP = context.ReadValue<Vector2>();
            isMoving = true;
        }
        else if (context.canceled)
        {
            _movementP = Vector2.zero;
            PlayerAnimationController.Instance.SetIdle();
            isMoving = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed && _movementP != Vector2.zero)
        {
            isRunning = true;
        }
        else if (context.canceled)
        {
            if(isMoving)
            {
                PlayerAnimationController.Instance.SetWalk();
            }
            else
            {
                PlayerAnimationController.Instance.SetIdle();
            }
            isRunning = false;
            Debug.Log("Running canceled");
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            string activeCamera = CameraController.Instance.GetActiveCameraName();

            if (activeCamera == ConstantValue.ThirdPerson)
            {
                activeCamera = ConstantValue.FirstPerson;
                PlayerAnimationController.Instance.ActiveAim();
            }
            else
            {
                activeCamera = ConstantValue.ThirdPerson;
                PlayerAnimationController.Instance.DeactiveAim();
            }

            CameraController.Instance.SetCameraByName(activeCamera);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(isCrouching)
            {
                PlayerAnimationController.Instance.DeactiveCrouch();
                isCrouching = false;
            }
            else
            {
                PlayerAnimationController.Instance.ActiveCrouch();
                isCrouching = true;
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlayerAnimationController.Instance.ActiveJump();
        }
    }

    public void OnDance(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlayerAnimationController.Instance.ActiveDance();
        }
    }
}