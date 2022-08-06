using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class EnemyDeath : MonoBehaviour
{
    Health health;
    Controller _controller;
    [SerializeField]
    private int points;
    private WaitForSeconds dieSeconds;
    private float dieTime = 1f;

    private void Awake()
    {
        _controller = GetComponent<Controller>();
        dieSeconds = new WaitForSeconds(dieTime);
        health = GetComponent<Health>();
    }

    public void HandleDeath()
    {
        _controller.Animator.SetTrigger("Dying");
        _controller.enabled = false;
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
        _controller.enabled = true;
        health.onDeath += HandleDeath;
    }

    private void OnDisable()
    {
        health.onDeath -= HandleDeath;
    }
}
