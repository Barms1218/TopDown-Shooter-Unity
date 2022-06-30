using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;

    private int _health;
    private Animator _animator;
    private Rigidbody2D _body2d;
    public static UnityAction<GameObject> OnDied;

    float IDamageable.Health 
    { 
        get => _health; 
        set => _health = (int)value; 
    }

    public int MaxHealth => maxHealth;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = maxHealth;
        _body2d = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, GameObject damageSource, float attackStrength)
    {
        _health -= damage;
        var pushDirection = gameObject.transform.position - damageSource.transform.position;
        _body2d.AddForce(pushDirection.normalized * attackStrength, ForceMode2D.Impulse);
        _animator.SetTrigger("Hurt");
        if (_health <= 0)
        {
            OnDied?.Invoke(this.gameObject);
        }
    } 
    public void RestoreHealth(int amount)
    {
        if (_health < maxHealth)
        {
            _health += amount;
            if (_health > maxHealth)
            {
                _health = maxHealth;
            }
        }

    }
}
