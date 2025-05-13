using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackState", menuName = "StatesSO/Attack")]
public class AttackState : StateSO
{
    public override void OnStateEnter(EnemyController ec)
    {
    }

    public override void OnStateExit(EnemyController ec)
    {
    }

    public override void OnStateUpdate(EnemyController ec)
    {
        HealthController healthController = GameObject
                                            .Find(ConstantValue.PlayerTag)
                                            .GetComponent<HealthController>();
        if (healthController != null)
        {
            healthController.TakeDamage(0.5f); // Deal 10 damage to player
            Debug.Log("Player Hit!");
        }
    }
}
