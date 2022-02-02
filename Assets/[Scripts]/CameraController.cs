using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float mouseSensitivity = 10.0f;
	public Transform playerTransform;
	private float xRotation = 0.0f;

	void Start()
	{
		//Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

		xRotation += -mouseY;
		xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

		transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
		playerTransform.Rotate(Vector3.up * mouseX);
	}
}
