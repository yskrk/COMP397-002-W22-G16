/*
 * ShotgunController.cs
 * Joshua Eagles - 301078033
 * Weihao Cai	- 301005651
 * Last Modified: 2022-03-20
 * 
 * Handles the logic for the shotgun, both the knockback blast needed for movement and the ammo system.
 * 
 * Revision History:
 * 2022-01-28 - Initial Creation, basic unfinished shotgun movement logic
 * 2022-02-01 - Refactor and clean up shotgun movement logic
 * 2022-02-10 - Add shotgun ammo functionality
 * 2022-02-12 - Documentation comments
 * 2022-02-12 - Shotgun SFX
 * 2022-03-06 - Shoot and destroy enemies
 * 2022-03-18 - Add B Button for shoot
 * 2022-03-20 - Adjust touch screen shooting logic
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

	// Play the shotgun firing sound
	private void PlayFireSound()
	{
		audioSource.clip = fireSound;
		audioSource.Play();
	}

	// Play the shotgun reloading sound
	private void PlayReloadSound()
	{
		audioSource.clip = reloadSound;
		audioSource.Play();
	}

	private void CheckForHitEnemies()
	{
		bool isHit = Physics.Raycast(player.transform.position, player.transform.forward, out RaycastHit hit, 10f, LayerMask.GetMask("Ground", "Enemy"));
		if (isHit && hit.collider.gameObject.CompareTag("Enemy"))
		{
			player.uiControls.RecordEnemyKilled(hit.collider.gameObject.name);
			Destroy(hit.collider.gameObject);
		}
	}

	public void onFireButtonPressed()
	{
		// When you have ammo and click the fire button, give knockback, subtract 1 ammo, and update the ammo display
		// Left Mouse Down is used to check that this is the initial press and not the release, unity calls this for both for some reason
		if (Input.GetMouseButtonDown(0) && remainingShots > 0)
		{
			player.velocity += -player.FacingDirection * knockbackAmount;

			rechargeTimerStart = Time.time;
			remainingShots -= 1;
			UpdateAmmoDisplay(remainingShots);
			PlayFireSound();

			CheckForHitEnemies();
		}
	}
}
