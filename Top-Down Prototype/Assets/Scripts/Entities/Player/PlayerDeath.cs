using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class PlayerDeath : MonoBehaviour
{
    [SerializeField] PlayerController _controller;
    Health _health;
    public static UnityAction gameOver;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _health = GetComponent<Health>();
    }

    private void HandleDeath()
    {
        //_controller.Animator.SetTrigger("Dead");
        //_controller.Collider.enabled = false;
        _controller.enabled = false;
        StartCoroutine(End());

    }

    private IEnumerator End()
    {
        
        yield return new WaitForSeconds(2f);
        gameOver?.Invoke();
        gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        _health.onDiedEvent += HandleDeath;
    }

    private void OnDisable()
    {
        _health.onDiedEvent -= HandleDeath;
    }
}
