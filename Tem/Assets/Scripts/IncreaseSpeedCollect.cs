using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedCollect : CollectableObject
{
	protected override void OnPickUp()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().MoveSpeed += 0.4f;
	}

	public override void OnDelete()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().MoveSpeed -= 0.4f;
		Destroy(gameObject);
	}
}
