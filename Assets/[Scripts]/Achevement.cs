using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achevement : Observer
{
	public AchievementSaveData achievementSaveData = new();

	// Start is called before the first frame update
	public void Start()
	{
		// register point in scene
		foreach (var p in FindObjectsOfType<Point>())
		{
			p.RegisterObserver(this);
		}

		GetComponent<AchievementDataSubject>().EmitAchievementDataUpdated(achievementSaveData);
	}

	public override void OnNotify(object value, NotificationType notificationType)
	{
		if (notificationType == NotificationType.AchievementUnlocked)
		{
			switch (value)
			{
				case "playerJumped":
					if (!achievementSaveData.playerJumped)
					{
						achievementSaveData.playerJumped = true;
						GetComponent<AchievementDataSubject>().EmitAchievementDataUpdated(achievementSaveData);
					}
					break;
				case "shotgunFired":
					if (!achievementSaveData.shotgunFired)
					{
						achievementSaveData.shotgunFired = true;
						GetComponent<AchievementDataSubject>().EmitAchievementDataUpdated(achievementSaveData);
					}
					break;
				case "enemyKilled":
					if (!achievementSaveData.enemyKilled)
					{
						achievementSaveData.enemyKilled = true;
						GetComponent<AchievementDataSubject>().EmitAchievementDataUpdated(achievementSaveData);
					}
					break;
				case "medkitCollected":
					if (!achievementSaveData.collectedMedkit)
					{
						achievementSaveData.collectedMedkit = true;
						GetComponent<AchievementDataSubject>().EmitAchievementDataUpdated(achievementSaveData);
					}
					break;
				case "healedWithMedkit":
					if (!achievementSaveData.healedWithMedkit)
					{
						achievementSaveData.healedWithMedkit = true;
						GetComponent<AchievementDataSubject>().EmitAchievementDataUpdated(achievementSaveData);
					}
					break;
			}
		}
	}
}

public enum NotificationType
{
	AchievementUnlocked,
	AchievementDataUpdated
}

public class AchievementSaveData
{
	public bool playerJumped = false;
	public bool shotgunFired = false;
	public bool enemyKilled = false;
	public bool collectedMedkit = false;
	public bool healedWithMedkit = false;

	public AchievementSaveData() { }

	public AchievementSaveData(bool playerJumped, bool shotgunFired, bool enemyKilled, bool collectedMedkit, bool healedWithMedkit)
	{
		this.playerJumped = playerJumped;
		this.shotgunFired = shotgunFired;
		this.enemyKilled = enemyKilled;
		this.collectedMedkit = collectedMedkit;
		this.healedWithMedkit = healedWithMedkit;
	}
}