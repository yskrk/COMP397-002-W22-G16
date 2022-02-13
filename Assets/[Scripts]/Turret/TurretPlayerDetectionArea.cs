using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlayerDetectionArea : MonoBehaviour
{
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			print("Turret detected Player!");
		}
	}
}
