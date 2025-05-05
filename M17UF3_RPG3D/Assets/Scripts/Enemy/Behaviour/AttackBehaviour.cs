using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    public float Damage;

    public void Attack(GameObject target)
    {
        HealthController healthController = target.GetComponent<HealthController>();
        if (healthController != null)
        {
            healthController.TakeDamage(Damage);
        }
    }
}
