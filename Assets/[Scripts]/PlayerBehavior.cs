/*
 * PlayerBehavior.cs
 * Joshua Eagles - 301078033
 * Last Modified: 2022-02-01
 * 
 * Handles the logic for the player moving around and jumping
 * 
 * Revision History: 
 * 2022-01-28 - Initial Creation, basic unfinished movement
 * 2022-02-01 - Further polish, more configuration options, and finalized movement code
 * 2022-02-12 - Documentation comments
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the logic for the player moving and jumping
public class PlayerBehavior : MonoBehaviour
{
	public CharacterController characterController;
	public CameraController cameraController;

	[Header("Movement")]
	public float maxSpeed = 10.0f;
	public float gravity = -30.0f;
	public float jumpHeight = 3.0f;
	public float maximumUpwardVelocity = 30f;
	public float maximumDownwardVelocity = -50f;
	public float accelerationFactor = 3f;
	public float groundedDecelerationFactor = 4f;
	public float aerialDecelerationFactor = 2f;
	public Vector3 velocity;

	[Header("Ground Detection")]
	public Transform groundCheck;
	public float groundRadius = 0.5f;
	public LayerMask groundMask;
	public bool isGrounded;

	// Returns a vector that points in the direction the player is looking, factoring in the camera as well
	public Vector3 FacingDirection
	{
		get
		{
			float cameraPitch = cameraController.transform.eulerAngles.x;
			return Quaternion.Euler(cameraPitch, 0, 0) * Vector3.forward;
		}
	}

	void Start()
	{
		// Get a reference to the character controller for later use
		characterController = GetComponent<CharacterController>();
	}

	void Update()
	{
		// Run ground check logic (needed as unity's built in ground check isn't very consistent)
		isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

		// Keep the player from building infinite speed when they're standing on the ground, and also add a terminal vertical velocity
		velocity.y = ConstrainGroundedVelocityY(isGrounded, velocity.y);
		velocity.y = Mathf.Clamp(velocity.y, maximumDownwardVelocity, maximumUpwardVelocity);

		// Get the movement input information and normalize it so diagonals don't make you move fasters
		Vector2 moveInput = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		moveInput = moveInput.normalized;

		// Use a different velocity interpolation factor based on the current state of the player (improves movement feel)
		float velocityInterpolationFactor;
		if (moveInput.sqrMagnitude != 0)
		{
			velocityInterpolationFactor = accelerationFactor;
		}
		else if (isGrounded)
		{
			velocityInterpolationFactor = groundedDecelerationFactor;
		}
		else
		{
			velocityInterpolationFactor = aerialDecelerationFactor;
		}

		// Calculate updated velocity
		velocity.x = Mathf.Lerp(velocity.x, maxSpeed * moveInput.x, velocityInterpolationFactor * Time.deltaTime);
		velocity.z = Mathf.Lerp(velocity.z, maxSpeed * moveInput.y, velocityInterpolationFactor * Time.deltaTime);

		// If you can jump and input one, jump
		if (Input.GetButton("Jump") && isGrounded)
		{
			velocity.y = CalculateJumpForce(jumpHeight, gravity);
		}

		// Apply gravity to the velocity
		velocity.y += gravity * Time.deltaTime;

		// Apply movement, rotate the velocity based on the players facing angle before applying it
		characterController.Move(RotateHorizontalVelocity(transform, velocity) * Time.deltaTime);
	}

	// Debug draw used to help visualize the ground check
	void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
	}

	// The calculation required to prevent the player from being endlessly accelerated by gravity while on the ground
	static float ConstrainGroundedVelocityY(bool isGrounded, float velocityY)
	{
		return (isGrounded && velocityY < 0.0f) ? -2.0f : velocityY;
	}

	// Calculation used for determining the force of a jump
	static float CalculateJumpForce(float jumpHeight, float gravity)
	{
		return Mathf.Sqrt(jumpHeight * -2.0f * gravity);
	}

	// Calculate the players horizontal velocity based on their facing direction
	static Vector3 RotateHorizontalVelocity(Transform transform, Vector3 velocity)
	{
		return new Vector3(0, velocity.y, 0) + transform.right * velocity.x + transform.forward * velocity.z;
	}
}
