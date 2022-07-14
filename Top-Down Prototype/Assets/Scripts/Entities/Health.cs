using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHaveHealth
{

    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private GameObject bloodParticle;
    private int _health;
    private Rigidbody2D _body2d;
    private Animator _animator;
    private OnDiedEvent onDiedEvent = new OnDiedEvent();
    public event UnityAction<bool> OnHit;
    private bool isDying = false;
    private bool isAngry = false;

    public int MaxHealth => maxHealth;
    public bool IsAngry => isAngry;
    int IHaveHealth.Health
    {
       get => _health;
       set => _health = (int)value; 
    }

    private void Awake()
    {
        _health = maxHealth;
        _body2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        EventManager.AddOnDiedEventInvoker(this);
    }
    public void TakeDamage(int amount, GameObject damageSource, float attackStrength)
    {
        if (_health > 0)
        {
            _health -= amount;
            Vector3 pushDirection = gameObject.transform.position - damageSource.transform.position;
            _body2d?.AddForce(pushDirection.normalized * attackStrength, ForceMode2D.Impulse);

            if (gameObject.CompareTag("Player") || gameObject.CompareTag("Enemy"))
            {
                var bloodSplatter = Instantiate(bloodParticle,
                    damageSource.transform.position, Quaternion.identity);
                AudioManager.Play(AudioClipName.BulletHit);
                Destroy(bloodSplatter, 0.5f);
            }
            _animator.SetTrigger("Hurt");
        }
        if (_health <= 0 && !isDying)
        {
            isDying = true;
            _health = 0;
            GetComponent<Collider2D>().enabled = false;
            onDiedEvent?.Invoke(this.gameObject);
        }

        if (!isAngry)
        {
            isAngry = true;
            OnHit?.Invoke(isAngry);
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

    public void AddOnDiedEventListener(UnityAction<GameObject> listener)
    {
        onDiedEvent.AddListener(listener);
    }

    private void OnDestroy()
    {
        EventManager.RemoveOnDiedInvoker(this);
    }
}
