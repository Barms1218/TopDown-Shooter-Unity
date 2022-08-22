using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI killCountText;
    [SerializeField] IntVariable killObject;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] IntVariable scoreObject;
    [SerializeField] TextMeshProUGUI finalTimeText;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Canvas gameOverCanvas;
    private SetTheCursor theCursor;
    PauseAction pause;


    private void Awake()
    {
        pause = new PauseAction();

        pause.Pause.PauseGame.started += _ => PauseGame();
    }
    private void Start()
    {
        //AudioManager.Play(AudioClipName.Gameplay_Music);
        theCursor = GameObject.FindObjectOfType<SetTheCursor>();
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void PauseGame()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0f;
            theCursor.ChangeCursor(theCursor.UICursor);
            pauseCanvas.enabled = true;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        theCursor.ChangeCursor(theCursor.AimCursor);
        pauseCanvas.enabled = false;
    }

    public void StartOver()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitGame()
    {
        pauseCanvas.enabled = false;
        ShowGameOverScreen();
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        MenuManager.GoToMenu(MenuName.Main);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    private void ShowGameOverScreen()
    {
        int score, killCount;
        score = scoreObject.Value;
        killCount = killObject.Value;
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        killCountText.text = "Enemies Killed: " + killCount.ToString();
        finalScoreText.text = "Final Score: " + score.ToString();
        finalTimeText.text = "You Survived For: " + GamePlayTimer.Instance.GameTime.text;
    }

    private void OnEnable()
    {
        pause.Enable();
    }

    private void OnDisable()
    {
        pause.Disable();
    }
}
