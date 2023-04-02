using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHP : MonoBehaviour
{
	private Rigidbody2D rb;
	public Transform player;

	public void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = GetComponent<Rigidbody2D>();
	}

	public void Update()
	{
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			Timer playerTimer = GameObject.Find("TimerCanvas").GetComponent<Timer>();
			if (playerTimer._timeLeft > 5f)
				playerTimer._timeLeft -= 5f; // убираем 5 секунд с таймера игрока
			else
				playerTimer._timeLeft = -1;
		}
	}
}
