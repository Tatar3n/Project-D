using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackController : MonoBehaviour
{
    // для атаки
    public Transform AttackPoint;
    public LayerMask DamageableLayerMask;
    public Animator anim;
    public float damage;
    public float AttackRange;
    public float TimeBtwAttack;
    public bool isAttacking = false;
    public float attackNPCTimer = 0f;

    private float _timer;

    // для блока
    public bool isBlocking = false;

    private void Update()
    {
        Attack();

        if (isAttacking)
        {
            attackNPCTimer -= Time.deltaTime;
            if (attackNPCTimer <= 0f)
            {
                isAttacking = false;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartBlocking();
        }

        if (Input.GetMouseButtonUp(1))
        {
            StopBlocking();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position,AttackRange);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        
    }

        private void Attack()
        {
        if (Time.timeScale != 0f)
            if (_timer<=0)
            {
                if (Input.GetMouseButtonDown(0) && !isBlocking)
                {
                    Collider2D[] enemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, DamageableLayerMask);
                    anim.SetBool("attack", true);
                    anim.Play("AttackHeroAnim");
                    if (enemies.Length != 0)
                    {
                        for (int i = 0; i < enemies.Length; i++)
                        {
                            isAttacking = true;
                            attackNPCTimer = 0.5f;
                            GameObject.Find(enemies[i].name).GetComponent<Enemy>().OnTriggerWithAttackingHero();
                            enemies[i].GetComponent<DamageableObject>().TakeDamage(damage);
                            
                        }
                    }

                    _timer = TimeBtwAttack;
                }
                else
                    anim.SetBool("attack", false);
            }
            else
            {
                _timer -= Time.deltaTime;
                
            }
       
    }

    void StartBlocking()
    {
        if (!isBlocking)
        {
            GetComponent<PlayerMovement>().anim.SetBool("stop", true);
            isBlocking = true;
        }
    }

    void StopBlocking()
    {
        if (isBlocking)
        {
            GetComponent<PlayerMovement>().anim.SetBool("stop", false);
            isBlocking = false;
        }
    }
}
