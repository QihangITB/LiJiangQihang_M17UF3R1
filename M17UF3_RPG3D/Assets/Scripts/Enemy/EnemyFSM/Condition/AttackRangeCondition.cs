using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackRangeCondition", menuName = "ConditionSO/AttackRange")]
public class AttackRangeCondition : ConditionSO
{
    public override bool CheckCondition(EnemyController ec)
    {
        return ec.OnAttackRange;
    }
}
