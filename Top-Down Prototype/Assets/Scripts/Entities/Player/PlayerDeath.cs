using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        _controller.enabled = false;
        _gameOver.Invoke();
        StartCoroutine(End());

    }

    private IEnumerator End()
    {
        _controller.Animator.SetTrigger("Dead");
        yield return new WaitForSeconds(2f);
    }
}
