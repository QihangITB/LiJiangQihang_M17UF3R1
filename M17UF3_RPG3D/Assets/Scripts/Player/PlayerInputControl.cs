using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Threading;

public class PlayerInputController : MonoBehaviour, InputControl.IPlayerActions
{
    private InputControl _ic;
    private Vector2 _movementP;
    private bool _isMoving = false;
    private bool _isRunning = false;
    private bool _isCrouching = false;

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
        if(_isMoving)
        {
            PlayerAnimationController.Instance.SetWalk();
        }

        if (_isRunning)
        {
            PlayerAnimationController.Instance.SetRun();
        }
    }

    void InputControl.IPlayerActions.OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _movementP = context.ReadValue<Vector2>();
            _isMoving = true;
        }
        else if (context.canceled)
        {
            _movementP = Vector2.zero;
            PlayerAnimationController.Instance.SetIdle();
            _isMoving = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed && _movementP != Vector2.zero)
        {
            _isRunning = true;
        }
        else if (context.canceled)
        {
            if(_isMoving)
            {
                PlayerAnimationController.Instance.SetWalk();
            }
            else
            {
                PlayerAnimationController.Instance.SetIdle();
            }
            _isRunning = false;
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
            if(_isCrouching)
            {
                PlayerAnimationController.Instance.DeactiveCrouch();
                _isCrouching = false;
            }
            else
            {
                PlayerAnimationController.Instance.ActiveCrouch();
                _isCrouching = true;
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

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            string inputValue = context.control.displayName;

            // Limpiamos primero el equipamiento
            InventoryController.Instance.RemoveItemInstance();

            switch (inputValue)
            {
                case "1":
                    InventoryController.Instance.CreateItemInstance(ConstantValue.Knife);
                    break;
                case "2":
                    InventoryController.Instance.CreateItemInstance(ConstantValue.Axe);
                    break;
                case "3":
                    InventoryController.Instance.CreateItemInstance(ConstantValue.Torch);
                    break;
                case "4":
                    InventoryController.Instance.CreateItemInstance(ConstantValue.Rifle);
                    break;
            }
        }
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Transform hud = transform.Find(ConstantValue.HUDCanvas);
            Transform menu = transform.Find(ConstantValue.MenuCanvas);

            if (menu.gameObject.activeSelf)
            {
                hud.gameObject.SetActive(true);
                menu.gameObject.SetActive(false);
            }
            else
            {
                hud.gameObject.SetActive(false);
                menu.gameObject.SetActive(true);
            }
        }
    }
}