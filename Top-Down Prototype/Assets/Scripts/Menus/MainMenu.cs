﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioClipObject menuMusic;

    private IEnumerator Start()
    {
        while (true)
        {
            AudioManager.Play(menuMusic);
            yield return new WaitForSeconds(330);
        }
    }

    #region Public methods

    /// <summary>
    /// Goes to the difficulty menu
    /// </summary>
    public void GoToGameplayScene()
    {
        SceneManager.LoadScene("Arena Scene");

    }

	/// <summary>
	/// Shows the help menu
	/// </summary>
	public void ShowHelp()
    {
		MenuManager.GoToMenu(MenuName.Help);
		//AudioManager.Play(AudioClipName.MenuButton);
	}

	/// <summary>
	/// Exits the game
	/// </summary>
	public void ExitGame()
    {
		Application.Quit();
    }


    #endregion
}
