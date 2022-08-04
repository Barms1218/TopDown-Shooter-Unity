using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class PlayerDeath : MonoBehaviour
{
    [SerializeField] PlayerController _controller;
    [SerializeField] UnityEvent _gameOver;
    [SerializeField] Health _health;

    private void Awake()
    {
        _health.onDeath += EndGame;
    }

    private void EndGame()
    {
        _controller.Animator.SetTrigger("Dead");
        GetComponent<Collider2D>().enabled = false;
        _controller.enabled = false;
        _gameOver.Invoke();
        StartCoroutine(End());

    }

    private IEnumerator End()
    {
        
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
