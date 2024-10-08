using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 1000;

    void Update()
    {

    }
    public void ReceiveDamage(float damage)
    {
        if (GulaMov.instance.gulaState == GulaMov.GulaState.Iddle)
        {
            maxHealth -= damage * 2;

            if (maxHealth < 0)
            {
                Death();
            }
        } else
        {
            maxHealth -= damage;

            if (maxHealth < 0)
            {
                Death();
            }
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
