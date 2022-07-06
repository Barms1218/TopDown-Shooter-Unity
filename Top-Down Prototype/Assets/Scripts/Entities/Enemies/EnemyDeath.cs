using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private int points;
    private Animator _animator;
    private EnemyMove _movement;
    private EnemyWeaponHandler _weapon;
    public static UnityAction<int> addPoints;

    void Awake()
    {
        _movement = GetComponent<EnemyMove>();
        _animator = GetComponent<Animator>();
        _weapon = GetComponent<EnemyWeaponHandler>();
        GetComponent<Health>().OnDied += HandleDeath;
    }

    private void HandleDeath()
    {
        if (_weapon != null)
        {
            _weapon.enabled = false;
        }
  

        _animator?.SetTrigger("Dying");


        if (_movement != null)
        {
            _movement.enabled = false;
        }
        AudioManager.Play(AudioClipName.ZombieInmateDeath);

        addPoints?.Invoke(points);
        Destroy(gameObject, 1.0f);

    }

    private void OnDestroy()
    {
        GetComponent<Health>().OnDied -= HandleDeath;
    }
}
