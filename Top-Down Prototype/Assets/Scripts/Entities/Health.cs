using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHaveHealth
{
    [SerializeField] FloatVariable health;
    private float _health;
    public UnityAction onDiedEvent;

    public int MaxHealth => health.MaxValue;

    public float CurrentHealth
    {
        get => _health;
    }

    private void Awake()
    {
        //_health = health.MaxValue;
    }

    public void ChangeHealth(int amount)
    {
        if (_health > 0)
        {
            _health += amount;
            if (TryGetComponent(out Animator _animator))
            {
                _animator.SetTrigger("Hurt");
            }
        }
        else if (_health <= 0)
        {
            onDiedEvent?.Invoke();
        }
    }

    private void OnEnable()
    {
        _health = health.MaxValue;
    }
}
