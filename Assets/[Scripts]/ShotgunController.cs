using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : MonoBehaviour
{
	public PlayerBehavior player;
	public Transform cameraTransform;

	public float knockbackAmount = 150.0f;

	void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			// player.velocity.z += -knockbackAmount;
			// Move this facing angle calculation to a function in playerbehavior
			float cameraPitch = cameraTransform.eulerAngles.x;
			Vector3 facingDirection = Quaternion.Euler(cameraPitch, 0, 0) * Vector3.forward;
			print(facingDirection);

			player.velocity += -facingDirection * knockbackAmount;
			//print(Mathf.Sin(cameraPitch));
			//player.velocity.y += Mathf.Sin(cameraPitch) * knockbackAmount;
		}
	}
}
