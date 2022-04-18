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
 * 2022-03-20 - Improved Saving and Loading
 */

using System;
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

	public Achevement achievementSystem;

	public PlayerBehavior playerBehavior;

	public bool IsPaused { get; private set; } = false;

	public Slider healthBar;
	public TMP_Text healthBarValueLabel;

	private int medKitCount;
	public TMP_Text medkitDisplayLabel;

	private List<string> killedEnemyNames = new();
	private List<string> collectedMedkitNames = new();

	void Start()
	{
		if (PlayerPrefs.GetInt("LoadSavedGame") == 1)
		{
			LoadGame();
		}

		PlayerPrefs.SetInt("LoadSavedGame", 0);
	}

	public void OnPauseButton_Pressed()
	{
		IsPaused = !IsPaused;
		ChangePausedState(IsPaused);
	}

	public void ChangePausedState(bool isPaused)
	{
		// Enable the correct UI container (and disable the game stuff if needed)
		gameplayUIContainer.SetActive(!isPaused);
		gameplayContainer.SetActive(!isPaused);
		// using scale instead of active so observers on the pause screen still function
		pauseMenuUIContainer.transform.localScale = isPaused ? Vector3.one : Vector3.zero;
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
				playerBehavior.transform.rotation.eulerAngles.y, medKitCount, (int)healthBar.value, collectedMedkitNames.ToArray(), killedEnemyNames.ToArray());

		string serializedSaveData = JsonUtility.ToJson(saveData);

		PlayerPrefs.SetString("SaveData", serializedSaveData);

		AchievementSaveData achievementSaveData = achievementSystem.achievementSaveData;
		string serializedAchievementSaveData = JsonUtility.ToJson(achievementSaveData);
		PlayerPrefs.SetString("AchievementSaveData", serializedAchievementSaveData);
	}

	public void OnLoadGameButton_Pressed()
	{
		// This button should simply reload the scene with the load game flag set, move the load code elsewhere and call it in Start()
		PlayerPrefs.SetInt("LoadSavedGame", 1);
		SceneManager.LoadScene("Gameplay");
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

		if (healthBar.value < 100)
		{
			playerBehavior.gameObject.GetComponent<Point>().HealedWithMedkit();
		}

		Heal(100);
		medkitDisplayLabel.text = medKitCount.ToString();
	}

	public void LoadGame()
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

		var enemyRobotPlayerDetections = Resources.FindObjectsOfTypeAll<EnemyRobotPlayerDetection>();
		foreach (var enemyRobotPlayerDetector in enemyRobotPlayerDetections)
		{
			if (Array.Exists<string>(saveData.killedEnemyNames, enemyName => enemyName == enemyRobotPlayerDetector.transform.parent.gameObject.name))
			{
				Destroy(enemyRobotPlayerDetector.transform.parent.gameObject);
			}
		}
		this.killedEnemyNames = new List<string>(saveData.killedEnemyNames);

		var medkits = Resources.FindObjectsOfTypeAll<MedKitController>();
		foreach (var medkit in medkits)
		{
			if (Array.Exists<string>(saveData.collectedMedkitNames, medkitName => medkitName == medkit.name))
			{
				Destroy(medkit.gameObject);
			}
		}
		this.collectedMedkitNames = new List<string>(saveData.collectedMedkitNames);

		healthBar.value = saveData.health;

		var serializedAchievementSaveData = PlayerPrefs.GetString("AchievementSaveData");
		var achievementSaveData = JsonUtility.FromJson<AchievementSaveData>(serializedAchievementSaveData);
		achievementSystem.achievementSaveData = achievementSaveData;
	}

	public void RecordEnemyKilled(string enemyName)
	{
		killedEnemyNames.Add(enemyName);
	}

	public void RecordMedkitCollected(string medkitName)
	{
		collectedMedkitNames.Add(medkitName);
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
	public string[] collectedMedkitNames;
	public string[] killedEnemyNames;

	public SaveData(float playerPositionX, float playerPositionY, float playerPositionZ, float playerRotationY, int medkitInventoryCount, int health, string[] collectedMedkitNames, string[] killedEnemyNames)
	{
		this.playerPositionX = playerPositionX;
		this.playerPositionY = playerPositionY;
		this.playerPositionZ = playerPositionZ;
		this.playerRotationY = playerRotationY;
		this.medkitInventoryCount = medkitInventoryCount;
		this.health = health;
		this.collectedMedkitNames = collectedMedkitNames;
		this.killedEnemyNames = killedEnemyNames;
	}
}
