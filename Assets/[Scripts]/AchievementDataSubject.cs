using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementDataSubject : Subject
{
	public void EmitAchievementDataUpdated(AchievementSaveData achievementSaveData)
	{
		Notify(achievementSaveData, NotificationType.AchievementDataUpdated);
	}
}
