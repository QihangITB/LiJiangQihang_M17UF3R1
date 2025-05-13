using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Caminar
    public void ActiveWalk()
    {
        _animator.SetBool(ConstantValue.WalkParam, true);
    }

    public void DeactiveWalk()
    {
        _animator.SetBool(ConstantValue.WalkParam, false);
    }

    // Perseguir
    public void ActiveChase()
    {
        _animator.SetBool(ConstantValue.ChaseParam, true);
    }

    public void DeactiveChase()
    {
        _animator.SetBool(ConstantValue.ChaseParam, false);
    }
}
