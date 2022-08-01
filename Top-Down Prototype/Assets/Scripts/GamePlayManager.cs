using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    PauseAction pause;

    [SerializeField] Canvas pauseCanvas;

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
            Time.timeScale = 0f;
            SetTheCursor.cursor.enabled = false;
            pauseCanvas.enabled = true;
        }
        else
        {
            Time.timeScale = 1f;
            SetTheCursor.cursor.enabled = true;
            pauseCanvas.enabled = false;
        }
    }

    public void StartOver()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Arena Scene");
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    private void OnEnable()
    {
        pause.Enable();
    }
}
