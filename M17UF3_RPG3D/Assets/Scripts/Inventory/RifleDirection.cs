using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleDirection : MonoBehaviour
{
    private Transform _left;
    private Transform _right;

    private void Start()
    {
        _left = InventoryController.Instance.LeftHand;
        _right = InventoryController.Instance.RightHand;
    }

    void FixedUpdate()
    {
        if(transform.parent != null)
        {
            transform.forward = _left.position - _right.position;
        }
    }
}
