using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class EnemyDeath : MonoBehaviour
{
    Health health;
    AIController _controller;
    [SerializeField] private int points;
    [SerializeField] IntVariable scoreObject;
    [SerializeField] IntVariable killCountObject;
    private WaitForSeconds dieSeconds;
    private float dieTime = 1f;

    private void Awake()
    {
        _controller = GetComponent<AIController>();
        dieSeconds = new WaitForSeconds(dieTime);
        health = GetComponent<Health>();
    }

    public void HandleDeath()
    {
        _controller.enabled = false;
        StartCoroutine(Die());
        AudioManager.Play(AudioClipName.ZombieInmateDeath);
    }

    private IEnumerator Die()
    {

        yield return dieSeconds;
        scoreObject.Value += points;
        killCountObject.Value++;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _controller.enabled = true;
        health.onDiedEvent += HandleDeath;
    }

    private void OnDisable()
    {
        health.onDiedEvent -= HandleDeath;
    }
}
