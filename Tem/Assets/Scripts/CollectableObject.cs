using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
	public GameObject sprite;
	private Inventory inventory;

	public void Start()
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			OnPickUp();

			
			GameObject.Find("Slot").GetComponent<Slot>().DestroyInSlot();
			Instantiate(sprite, inventory.slot.transform, false);
			Instantiate(gameObject, inventory.slot.transform, false);
			Destroy(gameObject);
			inventory.isFull = true;
			
		}
	}

	protected virtual void OnPickUp()
	{

	}

	public virtual void OnDelete()
	{

	}
}
