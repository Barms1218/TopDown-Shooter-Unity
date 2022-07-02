using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IHaveHealth
{
    [SerializeField] private int maxHealth = 100;
    private int _health;
    private Animator _animator;
    private Rigidbody2D _body2d;
    private Collider2D _collider;
    public event UnityAction OnDied;

    int IHaveHealth.Health 
    { 
        get => _health; 
        set => _health = (int)value; 
    }

    public int MaxHealth => maxHealth;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _health = maxHealth;
        _body2d = GetComponent<Rigidbody2D>();
        //EventManager.AddOnDiedEventInvoker(this);
    }

    public void TakeDamage(int damage, GameObject damageSource, float attackStrength)
    {
        _health -= damage;
        var pushDirection = gameObject.transform.position - damageSource.transform.position;
        _body2d?.AddForce(pushDirection.normalized * attackStrength, ForceMode2D.Impulse);
        _animator.SetTrigger("Hurt");
        if (isDying)
        {
            HandleDeath();
        }
    }

    public void HandleDeath()
    {
        var _movement = GetComponent<EnemyMove>();
        var weaponHandler = GetComponent<EnemyWeaponHandler>();
        if (weaponHandler != null)
        {
            weaponHandler.enabled = false;
        }
  

        _animator?.SetTrigger("Dying");
        Debug.Log(gameObject.name);

        OnDied?.Invoke();
        if (_movement != null)
        {
            _movement.enabled = false;
        }
        if (_collider != null)
        {
            _collider.enabled = false;
        }
        Destroy(gameObject, 1.0f);
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

    private bool isDying => _health <= 0;
    
}
