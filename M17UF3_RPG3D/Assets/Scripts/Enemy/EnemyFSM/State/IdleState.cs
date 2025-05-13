using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "IdleState", menuName = "StatesSO/Idle")]
public class IdleState : StateSO
{
    public override void OnStateEnter(EnemyController ec)
    {
        ec.MovementB.StartAgent();
    }

    public override void OnStateExit(EnemyController ec)
    {
        ec.MovementB.StopAgent();
    }

    public override void OnStateUpdate(EnemyController ec)
    {
        if (ec.HealthC.Health < ec.HealthC.MaxHealth)
        {
            ec.MovementB.PatrolASpecificPoint();
        }
        else
        {
            ec.MovementB.Patrol();
        }
    }
}
