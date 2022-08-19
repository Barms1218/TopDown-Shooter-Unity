using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] FloatVariable health;
    [SerializeField] IntVariable medkits;
    private PlayerControls inputActions;
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
        inputActions = new PlayerControls();
        _animator = GetComponentInChildren<Animator>();

        inputActions.Player.Use.started += _ => UseMedkit();
    }

    public void ChangeHealth(int amount)
    {
        if (_health > 0)
        {
            _health += amount;
            if (amount < 0)
            {
                _animator.SetTrigger("Hurt");
            }
            else if (amount > 0)
            {
                if (_health > health.Value)
                {
                    _health = health.Value;
                }
            }

        }
        else if (_health <= 0)
        {
            Die();
            _animator.SetTrigger("Dead");
        }

        var bloodSplatter = BloodPool.SharedInstance.GetPooledObject();
        if (bloodSplatter != null)
        {
            bloodSplatter.transform.SetPositionAndRotation(
                transform.position, transform.rotation);
            bloodSplatter.SetActive(true);
        }
    }

    private void UseMedkit()
    {
        if (medkits.Value > 0)
        {
            ChangeHealth(25);
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
