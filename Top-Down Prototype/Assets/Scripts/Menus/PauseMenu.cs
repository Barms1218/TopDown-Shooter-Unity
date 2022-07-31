using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The pause menu
/// </summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void OnEnable()
    {
        // pause the game when added to the scene
        Time.timeScale = 0;
    }

    /// <summary>
    /// Exits the application
    /// </summary>
    public void QuitToDesktop()
    {
        Application.Quit();
    }

    /// <summary>
    /// Quits the paused game
    /// </summary>
    public void QuitToMainMenu()
    {
        // unpause game, destroy menu, and go to main menu
        Time.timeScale = 1;
        gameObject.SetActive(false);
        MenuManager.GoToMenu(MenuName.Main);
    }
}
