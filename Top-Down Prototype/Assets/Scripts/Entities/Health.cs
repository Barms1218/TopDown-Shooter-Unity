using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHaveHealth
{

    [SerializeField] private int maxHealth = 100;
    HealthBar healthBar;
    private float _health;
    public UnityAction onDeath;
    public UnityAction<float> onHit;

    public int MaxHealth => maxHealth;

    float IHaveHealth.Health
    {
       get => _health;
       set => _health = value; 
    }

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.MaxValue = maxHealth;
        //healthBar.CurrentValue = maxHealth;
    }

    public void ReduceHealth(int amount, GameObject damageSource)
    {
        if (_health > 0)
        {
            _health -= amount;
            onHit?.Invoke(_health);
            //healthBar.CurrentValue = _health;
            if (gameObject.CompareTag("Player") || gameObject.CompareTag("Enemy"))
            {
                var bloodSplatter = BloodPool.SharedInstance.GetPooledObject();
                if (bloodSplatter != null)
                {
                    bloodSplatter.transform.SetPositionAndRotation(
                        damageSource.transform.position, damageSource.transform.rotation);
                    bloodSplatter.SetActive(true);
                }
                AudioManager.Play(AudioClipName.BulletHit);
            }
            if (TryGetComponent(out Animator _animator))
            {
                _animator.SetTrigger("Hurt");
            }
        }
        if (_health <= 0)
        {
            onDeath?.Invoke();
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

    private void OnEnable()
    {
        _health = maxHealth;
    }
}
