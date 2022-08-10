using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] FloatVariable health;
    private Animator _animator;
    private float _health;
    public UnityAction onDiedEvent;

    public float MaxHealth => health.Value;

    public float CurrentHealth
    {
        get => _health;
    }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void ChangeHealth(int amount)
    {
        if (_health > 0)
        {
            _health += amount;
            _animator.SetTrigger("Hurt");

            var bloodSplatter = BloodPool.SharedInstance.GetPooledObject();
            if (bloodSplatter != null)
            {
                bloodSplatter.transform.SetPositionAndRotation(
                    transform.position, transform.rotation);
                bloodSplatter.SetActive(true);
            }
        }
        else if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        onDiedEvent?.Invoke();
    }

    private void OnEnable()
    {
        _health = health.Value;
    }
}
