/*
 * ShotgunController.cs
 * Joshua Eagles - 301078033
 * Last Modified: 2022-02-10
 * 
 * Handles the logic for the shotgun, both the knockback blast needed for movement and the ammo system.
 * 
 * Revision History:
 * 2022-01-28 - Initial Creation, basic unfinished shotgun movement logic
 * 2022-02-01 - Refactor and clean up shotgun movement logic
 * 2022-02-10 - Add shotgun ammo functionality
 * 2022-02-12 - Documentation comments
 * 2022-02-12 - Shotgun SFX
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles the logic for the shotgun, both the knockback blast needed for movement and the ammo system
public class ShotgunController : MonoBehaviour
{
	public PlayerBehavior player;
	public RawImage crosshairDot1;
	public RawImage crosshairDot2;
	public Texture crosshairDotImage;
	public Texture crosshairDotFilledImage;

	public AudioClip fireSound;
	public AudioClip reloadSound;

	public float knockbackAmount = 150.0f;

	private const int totalShots = 2;
	private int remainingShots = 2;
	private float rechargeTimerStart = 0;
	public float rechargeDelayLength = 3f;

	private AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		// If there's ammo to reload and the timer has completed, refill the shotgun ammo and update the ammo display
		if (remainingShots < totalShots && Time.time > rechargeTimerStart + rechargeDelayLength)
		{
			remainingShots = totalShots;

			UpdateAmmoDisplay(remainingShots);
			PlayReloadSound();
		}

		// When you have ammo and click left mouse, give knockback, subtract 1 ammo, and update the ammo display
		if (Input.GetMouseButtonDown(0) && remainingShots > 0)
		{
			player.velocity += -player.FacingDirection * knockbackAmount;

			rechargeTimerStart = Time.time;
			remainingShots -= 1;
			UpdateAmmoDisplay(remainingShots);
			PlayFireSound();
		}
	}

	// Assign the correct crossHair dot images based on the amount of remaining ammo 
	private void UpdateAmmoDisplay(int newAmmoAmount)
	{
		switch (newAmmoAmount)
		{
			case 0:
				crosshairDot1.texture = crosshairDotFilledImage;
				crosshairDot2.texture = crosshairDotFilledImage;
				break;
			case 1:
				crosshairDot1.texture = crosshairDotImage;
				crosshairDot2.texture = crosshairDotFilledImage;
				break;
			default:
				crosshairDot1.texture = crosshairDotImage;
				crosshairDot2.texture = crosshairDotImage;
				break;
		}
	}

	private void PlayFireSound()
	{
		// if (audioSource.isPlaying) {
		// 	audioSource.Stop();
		// }
		audioSource.clip = fireSound;
		audioSource.Play();
	}

	private void PlayReloadSound()
	{
		audioSource.clip = reloadSound;
		audioSource.Play();
	}
}
