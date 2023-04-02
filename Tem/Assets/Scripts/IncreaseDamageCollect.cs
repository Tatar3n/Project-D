using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamageCollect : CollectableObject
{
    protected override void OnPickUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<MeleeAttackController>().damage += 5;
    }

	public override void OnDelete()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<MeleeAttackController>().damage -= 5;
		Destroy(gameObject);
	}
}
