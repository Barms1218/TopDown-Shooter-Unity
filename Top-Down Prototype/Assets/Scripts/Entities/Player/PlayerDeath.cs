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
        Time.timeScale = 0;
        _gameOver.Invoke();
    }

}
