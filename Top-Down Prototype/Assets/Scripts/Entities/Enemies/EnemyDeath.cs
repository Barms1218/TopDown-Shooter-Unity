using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class EnemyDeath : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] private int points;
    private WaitForSeconds dieSeconds;
    private float dieTime = 1f;

    public UnityEvent disableEnemy;
    public UnityEvent enableEnemy;

    private void Start()
    {
        dieSeconds = new WaitForSeconds(dieTime);

    }

    public void HandleDeath()
    {
        disableEnemy?.Invoke();
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
        enableEnemy?.Invoke();
        health.onDeath += HandleDeath;
    }

    private void OnDisable()
    {
        health.onDeath -= HandleDeath;
    }
}
