using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    private static GamePlayManager _instance;
    PauseAction pause;

    [SerializeField] Canvas pauseCanvas;
    private int killCount;

    public static GamePlayManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Game's broken");
            }
            return _instance;
        }
    }

    public int KillCount => killCount;

    private void Awake()
    {
        _instance = this;
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

    public void UpdateKillCount() => killCount++;

    private void OnEnable() => pause.Enable();
}
