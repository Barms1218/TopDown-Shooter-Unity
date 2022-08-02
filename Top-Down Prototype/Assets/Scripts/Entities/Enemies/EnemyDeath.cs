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
    private WaitForSeconds dieSeconds;
    private float dieTime = 1f;

    private void Start()
    {
        dieSeconds = new WaitForSeconds(dieTime);
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
        StartCoroutine(Die());
        AudioManager.Play(AudioClipName.ZombieInmateDeath);
    }

    private IEnumerator Die()
    {

        yield return dieSeconds;
        HUD.Instance.UpdatePointsText(points);
        GamePlayManager.Instance.UpdateKillCount();
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
