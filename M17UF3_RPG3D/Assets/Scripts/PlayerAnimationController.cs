using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private const string WalkParam = "isWalk", RunParam = "isRun", JumpParam = "Jump";

    public static PlayerAnimationController Instance;

    private List<string> _allParams;
    private Animator _animator;

    private void Awake()
    {
        Instance = this;
        _animator = GetComponent<Animator>();
        _allParams = new List<string> { WalkParam, RunParam, JumpParam };
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
        _animator.SetBool(RunParam, value);
    }

    public void SetWalk(bool value)
    {
        DisableAnimation();
        _animator.SetBool(WalkParam, value);
    }

    public void ActiveJump()
    {
        DisableAnimation();
        _animator.SetTrigger(JumpParam);
    }


}
