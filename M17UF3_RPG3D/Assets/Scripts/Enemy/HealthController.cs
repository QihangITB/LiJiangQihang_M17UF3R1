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

    private void Update()
    {
        if (gameObject.tag == ConstantValue.PlayerTag)
        {
            if (Health <= 0)
            {
                PlayerAnimationController.Instance.ActiveDie();
                
                StartCoroutine(ChangeCollider(1.7f));
            }
        }
    }

    private IEnumerator ChangeCollider(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.size = new Vector3(0f, 0.1f, 0f);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        HealthBar.value = Health;
    }
}
