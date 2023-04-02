using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Inventory inventory;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.isFull = false;
        }
    }

	public void DestroyInSlot()
	{
        if (inventory.isFull)
        {
            GetComponentInChildren<CollectableObject>().OnDelete();
        }

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        inventory.isFull = false;
    }
}
