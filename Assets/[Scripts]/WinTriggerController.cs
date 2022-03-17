/*
 * WinTriggerController.cs 
 * Joshua Eagles - 301078033
 * Last Modified: 2022-03-04
 * 
 * Handles the logic for the Win Trigger
 *
 * Revision History:
 * 2022-03-04 - Initial Creation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTriggerController : MonoBehaviour
{
	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.GetComponent<PlayerBehavior>())
		{
			SceneManager.LoadScene("WinScreen");
		}
	}
}
