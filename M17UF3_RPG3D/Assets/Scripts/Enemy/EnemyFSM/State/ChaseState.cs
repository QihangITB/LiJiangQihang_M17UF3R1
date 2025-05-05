using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChaseState", menuName = "StatesSO/Chase")]
public class ChaseState : StateSO
{
    public override void OnStateEnter(EnemyController ec)
    {
    }

    public override void OnStateExit(EnemyController ec)
    {
        ec.MovementB.Chase(ec.Target.transform);
    }

    public override void OnStateUpdate(EnemyController ec)
    {
        ec.MovementB.Chase(ec.Target.transform);
    }
}
