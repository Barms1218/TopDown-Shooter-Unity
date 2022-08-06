using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    private static GamePlayManager _instance;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] TextMeshProUGUI killCountText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI finalTimeText;
    [SerializeField] Canvas gameOverCanvas;
    private SetTheCursor theCursor;
    PauseAction pause;

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

    private void Awake()
    {
        _instance = this;
        pause = new PauseAction();

        pause.Pause.PauseGame.started += _ => PauseGame();
    }
    private void Start()
    {
        //AudioManager.Play(AudioClipName.Gameplay_Music);
        theCursor = GameObject.FindObjectOfType<SetTheCursor>();
    }

    public void PauseGame()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0f;
            theCursor.ChangeCursor(theCursor.UICursor);
            pauseCanvas.enabled = true;
        }
        else
        {
            Time.timeScale = 1f;
            theCursor.ChangeCursor(theCursor.AimCursor);
            pauseCanvas.enabled = false;
        }
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

    public void UpdateKillCount()
    {
        killCount++;
    }

    private void ShowGameOverScreen()
    {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        killCountText.text = "Enemies Killed: " + killCount.ToString();
        finalScoreText.text = "Final Score: " + HUD.Instance.Score;
        finalTimeText.text = "You Survived For: " + GamePlayTimer.Instance.GameTime.text;
    }

    private void OnEnable()
    {
        pause.Enable();
        PlayerDeath.gameOver += ShowGameOverScreen;
    }

    private void OnDisable()
    {
        pause.Disable();
    }
}
