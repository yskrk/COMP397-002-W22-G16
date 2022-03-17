/*
 * CrumblePlatformController.cs
 * Joshua Eagles - 301078033
 * Last Modified: 2022-03-05
 * 
 * Handles the logic for the crumble platforms
 * 
 * Revision History: 
 * 2022-03-05 - Initial Creation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblePlatformController : MonoBehaviour
{
	Animator crumblePlatformAnimator;
	const float fallAnimationLength = 1f;

	float animationEndTime = -1;

	void Start()
	{
		crumblePlatformAnimator = GetComponentInParent<Animator>();
	}

	void Update()
	{
		if (animationEndTime > 0 && Time.time > animationEndTime)
		{
			Destroy(transform.parent.gameObject);
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		var playerBehavior = collider.gameObject.GetComponent<PlayerBehavior>();
		if (playerBehavior != null)
		{
			crumblePlatformAnimator.Play("CrumblePlatformFall");

			animationEndTime = Time.time + fallAnimationLength;
		}
	}
}
