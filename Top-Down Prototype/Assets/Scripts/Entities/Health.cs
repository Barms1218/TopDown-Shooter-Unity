using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHaveHealth
{

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject bloodParticle;
    [SerializeField] UnityEvent onDeath;
    private float _health;
    private bool isAngry = false;

    public int MaxHealth => maxHealth;
    public bool IsAngry => isAngry;
    float IHaveHealth.Health
    {
       get => _health;
       set => _health = value; 
    }

    private void Awake()
    {
        _health = maxHealth;
    }
    public void ReduceHealth(float amount, GameObject damageSource)
    {
        if (_health > 0)
        {
            _health -= amount;
            //Vector3 pushDirection = gameObject.transform.position - damageSource.transform.position;


            if (gameObject.CompareTag("Player") || gameObject.CompareTag("Enemy"))
            {
                var bloodSplatter = Instantiate(bloodParticle,
                    damageSource.transform.position, Quaternion.identity);
                AudioManager.Play(AudioClipName.BulletHit);
                Destroy(bloodSplatter, 0.5f);
            }
            if (TryGetComponent(out Animator _animator))
            {
                _animator.SetTrigger("Hurt");
            }
        }
        if (_health <= 0)
        {
            _health = 0;
            onDeath.Invoke();
            GetComponent<Collider2D>().enabled = false;
        }


    }

    public void RestoreHealth(float amount)
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
