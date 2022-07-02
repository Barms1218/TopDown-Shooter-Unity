using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IHaveHealth
{
    [SerializeField] private int maxHealth = 100;
    OnDiedEvent onDiedEvent = new OnDiedEvent();
    private int _health;
    private Animator _animator;
    private Rigidbody2D _body2d;
    public static UnityAction<EnemyHealth> OnDied;

    int IHaveHealth.Health 
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
        //EventManager.AddOnDiedEventInvoker(this);
    }

    public void TakeDamage(int damage, GameObject damageSource, float attackStrength)
    {
        _health -= damage;
        var pushDirection = gameObject.transform.position - damageSource.transform.position;
        _body2d.AddForce(pushDirection.normalized * attackStrength, ForceMode2D.Impulse);
        _animator.SetTrigger("Hurt");
        if (_health <= 0)
        {
            HandleDeath();
        }
    }

    public void HandleDeath()
    {
        var dyingObjectMovement = GetComponent<EnemyMove>();
        var dyingObjectAnimator = GetComponent<Animator>();
        var dyingObjectCollider = GetComponent<Collider2D>();
        dyingObjectMovement.enabled = false;
        //GetComponent<MeleeAttack>().enabled = false;
        dyingObjectAnimator.SetTrigger("Dying");
        Debug.Log(gameObject.name);

        GetComponent<DropPickUp>().DropAmmo();
        dyingObjectCollider.enabled = false;
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
    
    public void AddOnDiedEventListener(UnityAction listener)
    {
        onDiedEvent.AddListener(listener);
    }

    private bool isDying => _health <= 0;
    
}
