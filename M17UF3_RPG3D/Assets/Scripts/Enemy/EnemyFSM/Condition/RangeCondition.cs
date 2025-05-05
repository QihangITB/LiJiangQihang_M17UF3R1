using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RangeCondition", menuName = "ConditionSO/Range")]
public class RangeCondition : ConditionSO
{
    public override bool CheckCondition(EnemyController ec)
    {
        return ec.OnRange && !ec.OnAttackRange;
    }
}
