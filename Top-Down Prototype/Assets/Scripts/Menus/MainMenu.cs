﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    #region Public methods

    private void Start()
    {
        //AudioManager.Play(AudioClipName.Gameplay_Music);
    }
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
		AudioManager.Play(AudioClipName.MenuButton);
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
