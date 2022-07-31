using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class EnemyDeath : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Collider2D _collider;
    [SerializeField] EnemyMove movement;
    [SerializeField] EnemyWeaponHandler weaponHandler;
    [SerializeField] private int points;
    private WaitForSeconds reviveSeconds;
    private float reviveTime = 1f;
    public static UnityAction<int> GivePoints;

    private void Start()
    {
        reviveSeconds = new WaitForSeconds(reviveTime);
        health.onDeath += HandleDeath;
    }

    public void HandleDeath()
    {
        _collider.enabled = false;
        movement.enabled = false;
        if (weaponHandler != null)
        {
            weaponHandler.enabled = false;
        }
        if (TryGetComponent<Animator>(out Animator _animator))
        {
            _animator?.SetTrigger("Dying");
        }
        GivePoints?.Invoke(points);
        StartCoroutine(Die());
        AudioManager.Play(AudioClipName.ZombieInmateDeath);
    }

    private IEnumerator Die()
    {

        yield return reviveSeconds;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (!_collider.enabled)
        {
            _collider.enabled = true;
        }
        if (!movement.enabled)
        {
            movement.enabled = true;
        }
        if (TryGetComponent(out EnemyWeaponHandler weaponHandler))
        {
            weaponHandler.enabled = true;
        }
    }
}
