using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : Subject
{
	public void PlayerJumped()
	{
		Notify("playerJumped", NotificationType.AchievementUnlocked);
	}

	// launching by shotgun
	public void ShotgunFired()
	{
		Notify("shotgunFired", NotificationType.AchievementUnlocked);
	}

	public void EnemyKilled()
	{
		Notify("enemyKilled", NotificationType.AchievementUnlocked);
	}

	public void MedkitCollected()
	{
		Notify("medkitCollected", NotificationType.AchievementUnlocked);
	}

	public void HealedWithMedkit()
	{
		Notify("healedWithMedkit", NotificationType.AchievementUnlocked);
	}
}
