using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobotPlayerDetectionRadius : MonoBehaviour
{
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			print("Enemy Robot detected Player!");
		}
	}
}
