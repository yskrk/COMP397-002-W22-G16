/*
 * OptionsUIControls.cs 
 * Joshua Eagles - 301078033
 * Ethan San Juan-Cheong - 301069513
 * Last Modified: 2022-02-13
 * 
 * Handles the logic for the controls on the options screen
 *
 * Revision History:
 * 2022-02-13 - Initial Creation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsUIControls : MonoBehaviour
{
	public void OnBackButton_Pressed()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
