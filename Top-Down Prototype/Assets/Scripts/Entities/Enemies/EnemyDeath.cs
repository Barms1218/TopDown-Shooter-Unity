using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private Animator _animator;
    private EnemyMove _movement;

    void Awake()
    {
        _movement = GetComponent<EnemyMove>();
        _animator = GetComponent<Animator>();
        GetComponent<Health>().OnDied += HandleDeath;
    }

    private void HandleDeath()
    {
        var weaponHandler = GetComponent<EnemyWeaponHandler>();
        if (weaponHandler != null)
        {
            weaponHandler.enabled = false;
        }
  

        _animator?.SetTrigger("Dying");
        Debug.Log(gameObject.name);


        if (_movement != null)
        {
            _movement.enabled = false;
        }
        AudioManager.Play(AudioClipName.ZombieInmateDeath);
        Destroy(gameObject, 1.0f);

    }

    private void OnDestroy()
    {
        GetComponent<Health>().OnDied -= HandleDeath;
    }
}
