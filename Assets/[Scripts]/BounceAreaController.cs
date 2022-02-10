using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAreaController : MonoBehaviour
{
	public float bounceForce = 50f;

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.name == "Player")
		{
			PlayerBehavior player = collider.gameObject.GetComponent<PlayerBehavior>();

			player.velocity.y = bounceForce;
		}
	}
}
