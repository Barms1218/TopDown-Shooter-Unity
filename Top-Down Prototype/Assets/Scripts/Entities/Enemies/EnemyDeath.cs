using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] GameObject _rifleAmmo;
    [SerializeField] GameObject _shotgunAmmo;

    private void Awake()
    {
        //Health.OnDeath += HandleDeath;
        //EventManager.AddHealthListener(HandleDeath);
    }

    private void HandleDeath()
    {
        var dyingObjectMovement = GetComponent<EnemyMove>();
        var dyingObjectAnimator = GetComponent<Animator>();
        var dyingObjectCollider = GetComponent<Collider2D>();
        dyingObjectMovement.enabled = false;
        //GetComponent<MeleeAttack>().enabled = false;
        dyingObjectAnimator.SetTrigger("Dying");
        Debug.Log(gameObject.name);

        int dropChance = Random.Range(1, 101);
        if (dropChance >= 0 && dropChance <= 50)
        {
            var rifleAmmoDrop = Instantiate(_rifleAmmo, transform.position,
             Quaternion.identity);
        }
        else if (dropChance >= 51 && dropChance <= 100)
        {
            var shotgunAmmoDrop = Instantiate(_shotgunAmmo, transform.position,
             Quaternion.identity);
        }
        dyingObjectCollider.enabled = false;
        Destroy(gameObject, 1.0f);

    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        //Health.OnDeath -= HandleDeath;
    }
}
