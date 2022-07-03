using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _body2d;
    private Collider2D _collider;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _body2d = GetComponent<Rigidbody2D>();
        GetComponent<Health>().OnDied += HandleDeath;
    }

    private void HandleDeath()
    {
        var _movement = GetComponent<EnemyMove>();
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
        if (_collider != null)
        {
            _collider.enabled = false;
        }
        Destroy(gameObject, 1.0f);

    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        GetComponent<Health>().OnDied -= HandleDeath;
    }
}
