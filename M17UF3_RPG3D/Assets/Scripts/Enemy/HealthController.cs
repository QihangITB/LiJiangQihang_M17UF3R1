using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float Health;
    public Slider HealthBar;

    private float _maxHealth;

    public float MaxHealth { get => _maxHealth; }

    private void Start()
    {
        _maxHealth = Health;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        HealthBar.value = Health;
    }
}
