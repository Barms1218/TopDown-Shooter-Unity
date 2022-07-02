using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHaveHealth
{
    [SerializeField] private int maxHealth = 100;
    private int _health;
    private Animator _animator;
    private Rigidbody2D _body2d;

    public int Health { get => _health; set { _health = value; } }

    public int MaxHealth => maxHealth;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = maxHealth;
        _body2d = GetComponent<Rigidbody2D>();
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

    public void TakeDamage(int amount, GameObject damageSource, float attackStrength)
    {
        _health -= amount;
        var pushDirection = gameObject.transform.position - damageSource.transform.position;
        _body2d.AddForce(pushDirection.normalized * attackStrength, ForceMode2D.Impulse);
        _animator.SetTrigger("Hurt");
        if (IsDying)
        {
            HandleDeath();
        }
    }
    public void HandleDeath()
    {
        Debug.Log("Crap I'm dead!");
    }

    private bool IsDying => _health <= 0;
}
