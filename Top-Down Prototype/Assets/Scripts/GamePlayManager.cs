using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePlayManager : MonoBehaviour
{
    PauseAction pause;

    private void Awake()
    {
        pause = new PauseAction();

        pause.Pause.PauseGame.started += _ => PauseGame();
    }

    private void Update()
    {

    }

    public void PauseGame()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        //MenuManager.GoToMenu(MenuName.Pause);
    }
    private void GameOver()
    {

    }


    private void OnEnable()
    {
        pause.Enable();
    }
}
