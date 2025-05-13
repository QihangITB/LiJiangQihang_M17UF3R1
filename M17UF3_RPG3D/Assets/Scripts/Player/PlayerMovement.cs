using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerInputController _iC;

    public float Speed = 5f;
    public float RunSpeed = 10f;
    public float NormalSpeed = 5f;

    private void Awake()
    {
        _iC = GetComponent<PlayerInputController>();
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Transform cam = Camera.main.transform;

        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDir = (camForward * _iC.MovementP.y + camRight * _iC.MovementP.x).normalized;

        _rb.velocity = new Vector3(moveDir.x * Speed, _rb.velocity.y, moveDir.z * Speed);

        if (moveDir != Vector3.zero)
        {
            transform.forward = moveDir;
        }
    }

    public IEnumerator JumpInTime(float time)
    {
        yield return new WaitForSeconds(time);
        _rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }
}
