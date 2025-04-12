using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public static PlayerAnimationController Instance;

    private List<string> _allParams;
    private Animator _animator;

    private void Awake()
    {
        Instance = this;
        _animator = GetComponent<Animator>();
        _allParams = new List<string>
        {
            ConstantValue.WalkParam,
            ConstantValue.RunParam,
            ConstantValue.JumpParam
        };
    }

    private void DisableAnimation()
    {
        foreach (string param in _allParams)
        {
            _animator.SetBool(param, false);
        }
    }

    public void SetRun(bool value)
    {
        DisableAnimation();
        _animator.SetBool(ConstantValue.RunParam, value);
    }

    public void SetWalk(bool value)
    {
        DisableAnimation();
        _animator.SetBool(ConstantValue.WalkParam, value);
    }

    public void ActiveJump()
    {
        DisableAnimation();
        _animator.SetTrigger(ConstantValue.JumpParam);
    }
}
