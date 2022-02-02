using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
	public CharacterController controller;
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
		controller = GetComponent<CharacterController>();
	}

	void Update()
	{
		isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

		velocity.y = ConstrainGroundedVelocityY(isGrounded, velocity.y);
		velocity.y = Mathf.Clamp(velocity.y, maximumDownwardVelocity, maximumUpwardVelocity);

		Vector2 moveInput = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		moveInput = moveInput.normalized;

		float interpolationFactor;
		if (moveInput.sqrMagnitude != 0)
		{
			interpolationFactor = accelerationFactor;
		}
		else if (isGrounded)
		{
			interpolationFactor = groundedDecelerationFactor;
		}
		else
		{
			interpolationFactor = aerialDecelerationFactor;
		}

		velocity.x = Mathf.Lerp(velocity.x, maxSpeed * moveInput.x, interpolationFactor * Time.deltaTime);
		velocity.z = Mathf.Lerp(velocity.z, maxSpeed * moveInput.y, interpolationFactor * Time.deltaTime);

		if (Input.GetButton("Jump") && isGrounded)
		{
			velocity.y = CalculateJumpForce(jumpHeight, gravity);
		}

		velocity.y += gravity * Time.deltaTime;

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
