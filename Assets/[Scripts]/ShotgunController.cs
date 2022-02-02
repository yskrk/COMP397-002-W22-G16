using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : MonoBehaviour
{
	public PlayerBehavior player;

	public float knockbackAmount = 150.0f;

	void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			player.velocity += -player.FacingDirection * knockbackAmount;
		}
	}
}
