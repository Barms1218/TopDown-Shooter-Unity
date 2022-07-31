using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePlayManager : MonoBehaviour
{
    PauseAction pause;

    [SerializeField] GameObject pauseCanvas;

    private void Awake()
    {
        pause = new PauseAction();

        pause.Pause.PauseGame.started += _ => PauseGame();
    }
    private void Start()
    {
        AudioManager.Play(AudioClipName.Gameplay_Music);
    }

    public void PauseGame()
    {
        if (Time.timeScale > 0)
        {
            //Time.timeScale = 0f;
            pauseCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseCanvas.SetActive(false);
        }
    }

    private void GameOver()
    {

    }


    private void OnEnable()
    {
        pause.Enable();
    }
}
