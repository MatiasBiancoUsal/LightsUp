using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;

    void Update()
    {
        
    }
    public void ReceiveDamage(float damage)
    {
        maxHealth -= damage;

        if (maxHealth < 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
