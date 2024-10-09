using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Slider healthSlider;
    public Animator animator;

    void Start()
    {
        healthSlider.value = maxHealth;
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }
    }

    public void ReceiveDamage(float damage)
    {
    
        if (GulaMov.instance.gulaState == GulaMov.GulaState.Iddle)
        {
            health -= damage * 2;

            if (health < 0)
            {
                Death();
            }
        } else
        {
            health -= damage;

            if (health < 0)
            {
                Death();
            }
        }

        if (health <= maxHealth / 2)
        {
            if (!GulaMov.instance.enraged) GulaMov.instance.enraged = true;
        }

    }

    void Death()
    {
        Destroy(gameObject);
    }

}
