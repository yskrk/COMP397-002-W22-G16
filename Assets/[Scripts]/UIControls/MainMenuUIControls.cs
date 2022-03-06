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
		SceneManager.LoadScene("Gameplay");
	}

	public void OnLoadGameButton_Pressed()
	{
		// Will be implemented in part 3
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
