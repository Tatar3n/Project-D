using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // rigidbody2d самого моба
    private Rigidbody2D rb;
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


    // Вспомогательное чсило для определения направления случайного сдвига моба
    int rnd;
    // Генератор случайных чисел
    System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        // Получить координаты игрока
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Если игрок попал в поле зрения - преследовать его
        if (isTargeted)
            ChasePlayer();
        // Если игрок не замечен - совершать случайные движения
        else
        {
            StartCoroutine(RandomMove());
        }
    }

    // Попадание игрока в поле зрения моба 
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Активировать преследование игрока, если он попал в поле зрения моба
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Вас заметили");
            isTargeted = true;
        }
    }

    // Преследование игрока
    public void ChasePlayer()
    {
        // Рассчитать реальное расстояние до игрока
        real_distance = Vector2.Distance(transform.position, player.position);
        // Задать рассчётное расстояние до игрока
        if (real_distance <= range)
            current_distance = real_distance;
        // Если игрок вышел за поле зрения - прекратить преследование
        else if (real_distance > range * 1.1f)
        {
            isTargeted = false;
            return;
        }
        else current_distance = range;
        // Рассчитать текущую скорость движения моба
        current_speed = speed * (float)(0.5 + ((current_distance / range) / 2.0)); ;

        // TODO: алгоритм движения моба в направлении игрока
        if (player.position.x > transform.position.x)
        {
           rb.velocity = transform.right * speed;
        }
            else if (player.position.x < transform.position.x)
        {
            rb.velocity = transform.right * -speed;
        }
    }

    // ?
    private IEnumerator RandomMove()
    {
        random = new System.Random();
        rnd = random.Next(-1,2);
        if (rnd == -1)
        {
            // Двигаться вправо 0.25 секунды
            rb.velocity = transform.right * speed;
            Invoke("nullify_speed", 0.5f);
        }
        else if (rnd == 1)
        {
            // Двигаться влево 0.25 секунды
            rb.velocity = transform.right * speed * (-1);
            Invoke("nullify_speed", 0.5f);
        }
        // Ожидать 5 секунд
        // Проверять каждые 0.5 секунд, не замечен ли игрок
        for (int i = 0; i < 10; i++)
        {
            if (isTargeted)
                yield break;
            else
                yield return new WaitForSeconds(0.5f);
        }
    }

    // Остановить моба
    void nullify_speed()
    {
        rb.velocity = transform.right * 0;
    }
}
