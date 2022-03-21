/*
 * MainMenuUIControls.cs 
 * Joshua Eagles - 301078033
 * Ethan San Juan-Cheong - 301069513
 * Last Modified: 2022-03-04
 * 
 * Handles the logic for the buttons on the main menu
 *
 * Revision History:
 * 2022-02-13 - Initial Creation
 * 2022-03-04 - Unlock cursor when entering this scene
 * 2022-03-20 - Load Game Flag Handling
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIControls : MonoBehaviour
{
	public void Start()
	{
		Cursor.lockState = CursorLockMode.None;
	}

	public void OnNewGameButton_Pressed()
	{
		PlayerPrefs.SetInt("LoadSavedGame", 0);
		SceneManager.LoadScene("Gameplay");
	}

	public void OnLoadGameButton_Pressed()
	{
		PlayerPrefs.SetInt("LoadSavedGame", 1);
		SceneManager.LoadScene("Gameplay");
	}

	public void OnOptionsButton_Pressed()
	{
		SceneManager.LoadScene("Options");
	}

	public void OnExitGameButton_Pressed()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif

		Application.Quit();
	}
}
