using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VisionBehaviour : MonoBehaviour
{
    const float Half = 2f;

    public Transform VisionLine;
    public float VisionAngle = 90f;

    private SphereCollider _sC;

    private void Start()
    {
        _sC = gameObject.GetComponentInChildren<SphereCollider>();
    }

    private void Update()
    {
        DrawVisionLines();
    }

    public bool IsInVision(GameObject player)
    {
        Vector3 origin = transform.position;
        Vector3 destiny = player.transform.position;
        origin.y = destiny.y = VisionLine.position.y;

        Vector3 directionToPlayer = (destiny - origin).normalized;

        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer <= VisionAngle / Half)
        {
            Physics.Raycast(origin, directionToPlayer, out RaycastHit hit, _sC.radius);

            if (hit.collider != null)
            {
                Debug.DrawLine(origin, hit.point, Color.yellow);
                Debug.Log("collision: " + hit.collider.gameObject.name);

                if (hit.collider.gameObject == player)
                {
                    return true;
                }
            }

        }
        return false;
    }

    void DrawVisionLines()
    {
        Vector3 leftDirection = Quaternion.Euler(0, -(VisionAngle/Half), 0) * transform.forward;
        Vector3 rightDirection = Quaternion.Euler(0, (VisionAngle / Half), 0) * transform.forward;

        Debug.DrawRay(transform.position, leftDirection * _sC.radius, Color.red);
        Debug.DrawRay(transform.position, rightDirection * _sC.radius, Color.red);
    }
}
