using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstantValue.PlayerTag))
        {
            _animator.SetTrigger(ConstantValue.OpenDoor);
        }
    } 

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ConstantValue.PlayerTag))
        {
            _animator.SetTrigger(ConstantValue.CloseDoor);
        }
    }
}
