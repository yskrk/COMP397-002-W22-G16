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
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the logic for capturing the cursor and rotating the camera to enable looking around the world
public class CameraController : MonoBehaviour
{
	public float mouseSensitivity = 10.0f;
	public Transform playerTransform;
	private float xRotation = 0.0f;
	public Joystick rightJoystick;

	void Start()
	{
		// Lock the cursor to the game window so you can freely use it to look around
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		// Get mouse movement information, and then use it to rotate the camera and player body to fascilitate looking around and movement
		/*float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;*/

		// Get mouse movement information, and then use it to rotate the camera and player body to fascilitate looking around and movement
		float mouseX = (Input.GetAxis("Mouse X")  * mouseSensitivity * Time.deltaTime + rightJoystick.Horizontal);
		float mouseY = (Input.GetAxis("Mouse Y")  * mouseSensitivity * Time.deltaTime + rightJoystick.Vertical);

		xRotation += -mouseY;
		xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

		transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
		playerTransform.Rotate(Vector3.up * mouseX);
	}
}
