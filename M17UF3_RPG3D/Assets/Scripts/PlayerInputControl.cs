using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Threading;

public class PlayerInputController : MonoBehaviour, InputControl.IPlayerActions
{
    private InputControl _ic;
    private Vector2 _movementP;
    private Rigidbody _rb;

    public Transform player;  // Referencia al jugador
    public float distanceFromPlayer = 5f;  // Distancia entre la cámara y el jugador
    private float mouseX = 0f;
    private float mouseY = 0f;

    private float _rotationX = 0f;

    public Transform Camera;
    public float MovementSpeed = 5f;
    public float CameraSpeed = 300f;

    private void Awake()
    {
        _ic = new InputControl();
        _ic.Player.SetCallbacks(this);
        _rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        MovePlayer();
    }

    private void Update()
    {
        MoveCameraWithMouse();
    }

    private void MovePlayer()
    {
        Vector3 forward = Camera.forward;
        Vector3 right = Camera.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * _movementP.y + right * _movementP.x).normalized;

        _rb.velocity = new Vector3(movement.x * MovementSpeed, _rb.velocity.y, movement.z * MovementSpeed);
    }

    private void MoveCameraWithMouse()
    {
        mouseX += Input.GetAxis("Mouse X") * CameraSpeed * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * CameraSpeed * Time.deltaTime;

        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0f);

        Vector3 targetPosition = new Vector3(0f, 2f, -distanceFromPlayer);
        Camera.transform.position = player.position + rotation * targetPosition;

        Camera.transform.LookAt(player);
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
}