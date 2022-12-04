using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackController : MonoBehaviour
{
    public Transform AttackPoint;
    public LayerMask DamageableLayerMask;
    public float damage;
    public float AttackRange;
    public float TimeBtwAttack;

    private float _timer;

    private void Update()
    {
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position,AttackRange);
    }

    private void Attack()
    {
        if (_timer<=0)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, DamageableLayerMask);

                if (enemies.Length != 0)
                {
                    for (int i = 0;i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<DamageableObject>().TakeDamage(damage);
                    }
                }

                _timer = TimeBtwAttack;
            }
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }
}
