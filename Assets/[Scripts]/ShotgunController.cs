using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunController : MonoBehaviour
{
	public PlayerBehavior player;
	public RawImage crosshairDot1;
	public RawImage crosshairDot2;
	public Texture crosshairDotImage;
	public Texture crosshairDotFilledImage;

	public float knockbackAmount = 150.0f;

	private const int totalShots = 2;
	private int remainingShots = 2;
	private float rechargeTimerStart = 0;
	public float rechargeDelayLength = 3f;

	void Update()
	{
		if (remainingShots < totalShots && Time.time > rechargeTimerStart + rechargeDelayLength)
		{
			remainingShots = totalShots;

			updateAmmoDisplay(remainingShots);
		}

		if (Input.GetMouseButtonDown(0) && remainingShots > 0)
		{
			player.velocity += -player.FacingDirection * knockbackAmount;

			rechargeTimerStart = Time.time;
			remainingShots -= 1;
			updateAmmoDisplay(remainingShots);
		}
	}

	// If we add more weapons something like this should be handled by a script attached to the crosshair
	private void updateAmmoDisplay(int newAmmoAmount)
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
}
