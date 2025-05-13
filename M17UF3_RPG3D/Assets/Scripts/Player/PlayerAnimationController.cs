using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public static PlayerAnimationController Instance;

    private Animator _animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _animator = GetComponent<Animator>();
    }

    // Agacharse
    public void ActiveCrouch()
    {
        _animator.SetBool(ConstantValue.CrouchParam, true);
    }

    public void DeactiveCrouch()
    {
        _animator.SetBool(ConstantValue.CrouchParam, false);
    }

    // Apuntar
    public void ActiveAim()
    {
        _animator.SetBool(ConstantValue.AimParam, true);
    }

    public void DeactiveAim()
    {
        _animator.SetBool(ConstantValue.AimParam, false);
    }

    // Saltar
    public void ActiveJump()
    {
        _animator.SetTrigger(ConstantValue.JumpTrigger);
    }

    // Morir
    public void ActiveDie()
    {
        _animator.SetTrigger(ConstantValue.DieTrigger);
    }

    // Bailar
    public void ActiveDance()
    {
        _animator.SetTrigger(ConstantValue.DanceTrigger);

        DeactiveAim();
        InventoryController.Instance.RemoveItemInstance();

        float danceTime = _animator.GetCurrentAnimatorStateInfo(0).length;
        CameraController.Instance.SetTemporalCameraByName(ConstantValue.FrontPerson, danceTime - ConstantValue.Offset);
    }

    // Atacar
    public void ActiveAttack()
    {
        _animator.SetTrigger(ConstantValue.AttackTrigger);
    }

    // Movimiento
    public void SetIdle()
    {
        _animator.SetFloat(ConstantValue.SpeedParam, 0.0f);
    }

    public void SetWalk()
    {
        _animator.SetFloat(ConstantValue.SpeedParam, 0.5f);
    }

    public void SetRun()
    {
        _animator.SetFloat(ConstantValue.SpeedParam, 1f);
    }

    // Otros
    public float GetCurrentAnimationTime()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).length;
    }
}
