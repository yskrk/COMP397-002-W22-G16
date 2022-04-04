/*
 * ShellBehavior.cs
 * Joshua Eagles - 301078033
 * Yusuke Kuroki - 301137023
 * Last Modified: 2022-04-03
 * 
 * Handles the despawning logic for the ejected shells
 * 
 * Revision History:
 * 2022-04-03 - Initial Creation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellBehaviour : MonoBehaviour
{
	private float aliveStartTime = -1f;

	private const float ALIVE_TIME = 10f;

	void Update()
	{
		// Hasn't been instantiated
		if (aliveStartTime == -1)
		{
			aliveStartTime = Time.time;
		}

		if (Time.time - aliveStartTime > ALIVE_TIME)
		{
			aliveStartTime = -1;
			gameObject.SetActive(false);
		}
	}
}
