/*
 * GameOverUIControls.cs 
 * Joshua Eagles - 301078033
 * Last Modified: 2022-02-13
 * 
 * Handles the logic for the controls on the game over screen
 *
 * Revision History:
 * 2022-02-13 - Initial Creation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIControls : MonoBehaviour
{
	public void OnRetryButton_Pressed()
	{
		SceneManager.LoadScene("Gameplay");
	}

	public void OnExitToMenuButton_Pressed()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
