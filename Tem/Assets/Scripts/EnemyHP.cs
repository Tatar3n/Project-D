using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : DamageableObject
{

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
