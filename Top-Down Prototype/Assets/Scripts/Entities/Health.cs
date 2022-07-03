using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHaveHealth
{

    [SerializeField] private int maxHealth = 100;
    private int _health;
    private Rigidbody2D _body2d;
    private Animator _animator;
    public event UnityAction OnDied;
    public int MaxHealth => maxHealth;

     int IHaveHealth.Health 
    { 
        get => _health; 
        set => _health = (int)value; 
    }

    private void Awake()
    {
        _body2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    public void TakeDamage(int amount, GameObject damageSource, float attackStrength)
    {
        _health -= amount;
        Debug.Log(_health);
        var pushDirection = gameObject.transform.position - damageSource.transform.position;
        _body2d?.AddForce(pushDirection.normalized * attackStrength, ForceMode2D.Impulse);
        _animator.SetTrigger("Hurt");
        if (_health <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            OnDied?.Invoke();
        }
    }

    public void HandleDeath()
    {
        throw new System.NotImplementedException();
    }

    public void RestoreHealth(int amount)
    {
        throw new System.NotImplementedException();
    }

    private bool IsDying => _health <= 0;
}
