using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
	public CharacterController controller;

	[Header("Movement")]
	public float maxSpeed = 10.0f;
	public float gravity = -30.0f;
	public float jumpHeight = 3.0f;
	public Vector3 velocity;

	[Header("Ground Detection")]
	public Transform groundCheck;
	public float groundRadius = 0.5f;
	public LayerMask groundMask;
	public bool isGrounded;

	void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	void Update()
	{
		isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

		velocity.y = ConstrainGroundedVelocityY(isGrounded, velocity.y);

		float inputX = Input.GetAxisRaw("Horizontal");
		float inputZ = Input.GetAxisRaw("Vertical");

		// Need to use a different factor for acceleration and deceleration, 0.1f is good for acceleration
		velocity.x = Mathf.Lerp(velocity.x, maxSpeed * inputX, 0.01f);
		velocity.z = Mathf.Lerp(velocity.z, maxSpeed * inputZ, 0.01f);

		if (Input.GetButton("Jump") && isGrounded)
		{
			velocity.y = CalculateJumpForce(jumpHeight, gravity);
		}

		velocity.y += gravity * Time.deltaTime;

		// This rotation thing doesn't work when dealing with other things affecting velocity, need to change approach
		controller.Move(RotateHorizontalVelocity(transform, velocity) * Time.deltaTime);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
	}

	static float ConstrainGroundedVelocityY(bool isGrounded, float velocityY)
	{
		return (isGrounded && velocityY < 0.0f) ? -2.0f : velocityY;
	}

	static float CalculateJumpForce(float jumpHeight, float gravity)
	{
		return Mathf.Sqrt(jumpHeight * -2.0f * gravity);
	}

	static Vector3 RotateHorizontalVelocity(Transform transform, Vector3 velocity)
	{
		return new Vector3(0, velocity.y, 0) + transform.right * velocity.x + transform.forward * velocity.z;
	}
}
