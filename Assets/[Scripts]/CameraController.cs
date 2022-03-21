/*
 * CameraController.cs
 * Joshua Eagles - 301078033
 * Last Modified: 2022-02-12
 * 
 * Handles the logic for capturing the cursor and rotating the camera to enable looking around the world.
 * 
 * Revision History:
 * 2022-01-28 - Initial Creation
 * 2022-02-01 - Rename playerBody to playerTransform for clarity
 * 2022-02-12 - Documentation comments and fix mouse sensitivity at arbitrary framerates
 * 2022-03-15 - Add Joystick 
 * 2022-03-20 - Adjust Joystick functionality
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float lookSensitivity = 10.0f;
	public Transform playerTransform;
	private float xRotation = 0.0f;
	public Joystick rightJoystick;

	void Update()
	{
		// Get right stick movement information, and then use it to rotate the camera and player body to fascilitate looking around and movement
		float lookX = (rightJoystick.Horizontal * lookSensitivity * Time.deltaTime);
		float lookY = (rightJoystick.Vertical * lookSensitivity * Time.deltaTime);

		xRotation += -lookY;
		xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

		transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
		playerTransform.Rotate(Vector3.up * lookX);
	}
}
