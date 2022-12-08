using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	// rigidbody2d самого моба
	public Rigidbody2D rigidbody;
	// Начальная скорость движения моба
	public float speed = 1;
	// Радиус поля зрения
	public float range = 10;
	// Игрок, которого нужно преследовать
	public Transform player;

	// Текущая скорость движения моба
	private float current_speed;
	// Текущее расстояние до игрока (рассчётное)
	private float current_distance;
	// Текущее расстояние до игрока (реальное)
	private float real_distance;
	// Попадание игрока в поле зрения моба
	private bool isTargeted;

	// Вспомогательное число для определения направления случайного сдвига моба
	int rnd;
	// Генератор случайных чисел
	System.Random random;

	void Start() {
	
	}

	void FixedUpdate() {
		// Попадание игрока в поле зрения
		if (Vector2.Distance(transform.position, player.position) <= range)
			isTargeted = true;

		// Если игрок попал в поле зрения - преследовать его
		if (isTargeted)
			ChasePlayer();
		// Если игрок не замечен - совершать случайные движения
		else {
//			StartCoroutine(RandomMove());
		}
	}

	// Преследование игрока
	public void ChasePlayer() {
		// Рассчитать реальное расстояние до игрока
		real_distance = Vector2.Distance(transform.position, player.position);
		// Задать рассчётное расстояние до игрока
		if (real_distance <= range)
			current_distance = real_distance;
		// Если игрок вышел за поле зрения - прекратить преследование
		else if (real_distance > range * 1.1f) {
			isTargeted = false;
			return;
		}
		else current_distance = range;
		// Рассчитать текущую скорость движения моба
		current_speed = speed * (float)(0.5 + ((current_distance / range) / 2.0)); ;

		if (player.position.x > transform.position.x) {
			rigidbody.velocity = new Vector2(speed, 0);
		}
		else if (player.position.x < transform.position.x) {
			rigidbody.velocity = new Vector2(-speed, 0);
		}
	}

	
	

//	private IEnumerator RandomMove() {
//		rnd = random.Next(-1, 1);
//		if (rnd == -1) {
//			// Двигаться вправо 1 секунду
//			rigidbody.velocity = new Vector2(speed, 0);
//			Invoke("nullify_speed", 1f);
//		}
//		else if (rnd == 1) {
//			// Двигаться влево 1 секунду
//			rigidbody.velocity = new Vector2(-speed, 0);
//			Invoke("nullify_speed", 1f);
//		}
//		// Ожидать 5 секунд
//		yield return new WaitForSeconds(5);
//	}

	// Остановить моба
	void nullify_speed() {
		rigidbody.velocity = new Vector2(0, 0);
	}
}
