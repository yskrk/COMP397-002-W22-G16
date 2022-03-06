/*
 * GameplayUIControls.cs 
 * Joshua Eagles - 301078033
 * Last Modified: 2022-03-05
 * 
 * Handles the logic for the buttons on the pause menu
 *
 * Revision History:
 * 2022-02-13 - Initial Creation
 * 2022-03-05 - Health Bar Logic
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameplayUIControls : MonoBehaviour
{
	public GameObject gameplayUIContainer;
	public GameObject gameplayContainer;
	public GameObject pauseMenuUIContainer;

	public bool IsPaused { get; private set; } = false;

	public Slider healthBar;
	public TMP_Text healthBarValueLabel;

	void Update()
	{
		// Handle the player pressing the pause button
		if (Input.GetButtonDown("Pause"))
		{
			IsPaused = !IsPaused;
			ChangePausedState(IsPaused);
		}
	}

	public void ChangePausedState(bool isPaused)
	{
		// Enable the correct UI container (and disable the game stuff if needed)
		gameplayUIContainer.SetActive(!isPaused);
		gameplayContainer.SetActive(!isPaused);
		pauseMenuUIContainer.SetActive(isPaused);

		// Make sure the cursor isn't locked while paused
		Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
	}

	public void OnResumeButton_Pressed()
	{
		IsPaused = false;
		ChangePausedState(IsPaused);
	}

	public void OnSaveGameButton_Pressed()
	{
		// Functionality will be added in part 3
	}

	public void OnLoadGameButton_Pressed()
	{
		// Functionality will be added in part 3
	}

	public void OnExitToMenu_Pressed()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void TakeDamage(int damage)
	{
		healthBar.value -= damage;
	}

	// The health bar value is what ultimately tracks the player's health
	// So doing logic for 0 health here makes sense so that it'll apply regardless of what changes it
	public void OnHealthBar_Changed()
	{
		if (healthBar.value <= 0)
		{
			SceneManager.LoadScene("GameOver");
		}

		healthBarValueLabel.text = healthBar.value.ToString();
	}
}
