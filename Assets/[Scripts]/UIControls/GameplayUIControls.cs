/*
 * GameplayUIControls.cs 
 * Joshua Eagles - 301078033
 * Last Modified: 2022-03-05
 * 
 * Handles the logic for the buttons on the pause menu
 *
 * Revision History:
 * 2022-02-13 - Initial Creation
 * 2022-03-05 - Health Bar Logic + Medkit logic
 * 2022-03-06 - Saving and Loading
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

	private int medKitCount;
	public TMP_Text medkitDisplayLabel;

	void Update()
	{
		// Handle the player pressing the pause button
		if (Input.GetButtonDown("Pause"))
		{
			IsPaused = !IsPaused;
			ChangePausedState(IsPaused);
		}

		if (Input.GetButtonDown("Medkit"))
		{
			UseMedKit();
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
		var playerBehavior = Resources.FindObjectsOfTypeAll<PlayerBehavior>()[0];
		var playerTransform = playerBehavior.gameObject.transform;

		SaveData saveData = new(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z,
				playerBehavior.transform.rotation.eulerAngles.y, medKitCount, (int)healthBar.value);

		string serializedSaveData = JsonUtility.ToJson(saveData);

		PlayerPrefs.SetString("SaveData", serializedSaveData);
	}

	public void OnLoadGameButton_Pressed()
	{
		if (!PlayerPrefs.HasKey("SaveData"))
		{
			return;
		}

		var serializedSaveData = PlayerPrefs.GetString("SaveData");
		var saveData = JsonUtility.FromJson<SaveData>(serializedSaveData);

		var playerBehavior = Resources.FindObjectsOfTypeAll<PlayerBehavior>()[0];
		playerBehavior.LoadEntity(saveData);

		medKitCount = saveData.medkitInventoryCount;
		medkitDisplayLabel.text = medKitCount.ToString();

		healthBar.value = saveData.health;
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

	public void Heal(int healAmount)
	{
		healthBar.value += healAmount;
	}

	public void AddMedKit()
	{
		medKitCount++;
		medkitDisplayLabel.text = medKitCount.ToString();
	}

	public void UseMedKit()
	{
		if (medKitCount <= 0)
		{
			return;
		}
		medKitCount--;
		Heal(100);
		medkitDisplayLabel.text = medKitCount.ToString();
	}
}

public class SaveData
{
	public float playerPositionX;
	public float playerPositionY;
	public float playerPositionZ;
	public float playerRotationY;
	public int medkitInventoryCount;
	public int health;

	public SaveData(float playerPositionX, float playerPositionY, float playerPositionZ, float playerRotationY, int medkitInventoryCount, int health)
	{
		this.playerPositionX = playerPositionX;
		this.playerPositionY = playerPositionY;
		this.playerPositionZ = playerPositionZ;
		this.playerRotationY = playerRotationY;
		this.medkitInventoryCount = medkitInventoryCount;
		this.health = health;
	}
}