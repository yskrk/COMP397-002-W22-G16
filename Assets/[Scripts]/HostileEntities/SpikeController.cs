/*
 * SpikeController.cs
 * Joshua Eagles - 301078033
 * Last Modified: 2022-03-05
 * 
 * Handles the logic for the hazardous spikes
 * 
 * Revision History: 
 * 2022-03-05 - Initial Creation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
	void OnTriggerEnter(Collider collider)
	{
		var playerBehavior = collider.gameObject.GetComponent<PlayerBehavior>();
		if (playerBehavior != null)
		{
			playerBehavior.TakeDamage(50);
		}
	}
}
