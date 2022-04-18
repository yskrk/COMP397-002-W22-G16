using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementStatusDisplay : Observer
{
	[SerializeField] string achievementName = "";

	public void Start()
	{
		// register point in scene
		foreach (var point in FindObjectsOfType<Point>())
		{
			point.RegisterObserver(this);
		}

		FindObjectOfType<AchievementDataSubject>().RegisterObserver(this);
	}

	public override void OnNotify(object value, NotificationType notificationType)
	{
		switch (notificationType)
		{
			case NotificationType.AchievementUnlocked:
				if (achievementName == (string)value)
				{
					SetStatusTextObtained();
				}
				break;
			case NotificationType.AchievementDataUpdated:
				if (value is AchievementSaveData)
				{
					var achievementSaveData = (AchievementSaveData)value;
					switch (achievementName)
					{
						case "playerJumped":
							if (achievementSaveData.playerJumped)
							{
								SetStatusTextObtained();
							}
							break;
						case "shotgunFired":
							if (achievementSaveData.shotgunFired)
							{
								SetStatusTextObtained();
							}
							break;
						case "enemyKilled":
							if (achievementSaveData.enemyKilled)
							{
								SetStatusTextObtained();
							}
							break;
						case "medkitCollected":
							if (achievementSaveData.collectedMedkit)
							{
								SetStatusTextObtained();
							}
							break;
						case "healedWithMedkit":
							if (achievementSaveData.healedWithMedkit)
							{
								SetStatusTextObtained();
							}
							break;
					}
				}
				break;
		}
	}

	void SetStatusTextObtained()
	{
		var label = GetComponent<TMP_Text>();
		if (label != null)
		{
			label.text = "OBTAINED";
		}
	}
}
