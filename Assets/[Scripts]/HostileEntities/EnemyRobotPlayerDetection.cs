/*
 * EnemyRobotPlayerDetection.cs
 * Joshua Eagles - 301078033
 * Last Modified: 2022-03-05
 * 
 * Handles the logic for enemy robot's detection
 * 
 * Revision History: 
 * 2022-02-20 - Initial Creation
 * 2022-03-06 - AI Mesh Navigation and Attacking the Player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRobotPlayerDetection : MonoBehaviour
{
	PlayerBehavior playerBehavior;
	Transform playerTransform = null;
	NavMeshAgent navMeshAgent;

	void Start()
	{
		navMeshAgent = GetComponentInParent<NavMeshAgent>();
		playerBehavior = FindObjectOfType<PlayerBehavior>();
		playerTransform = playerBehavior.gameObject.transform;
	}

	void Update()
	{
		if (playerTransform != null)
		{
			navMeshAgent.destination = playerTransform.transform.position;
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			playerBehavior.TakeDamage(25);
		}
	}
}
