/*
 * BounceAreaController.cs
 * Joshua Eagles - 301078033
 * Last Modified: 2022-02-10
 * 
 * Handles the logic for the bouncy platforms, giving the player a big vertical
 * boost when they land on it.
 * 
 * Revision History:
 * 2022-02-10 - Initial Creation
 * 2022-02-12 - Documentation comments
 * 2022-02-12 - Bounce Sound
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class countrols the bouncing area on top of the bouncy platform
public class BounceAreaController : MonoBehaviour
{
	public float bounceForce = 50f;
	private AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	// Handle the logic that occurs when the player enters the bounce area trigger
	private void OnTriggerEnter(Collider collider)
	{
		// When a body enters the trigger area, try and get the PlayerBehavior component
		// If they have it, they're the player, and we can also use the component to apply the velocity change
		PlayerBehavior player = collider.gameObject.GetComponent<PlayerBehavior>();
		if (player != null)
		{
			player.velocity.y = bounceForce;

			PlayBounceSound();
		}
	}

	private void PlayBounceSound()
	{
		audioSource.Play();
	}
}
