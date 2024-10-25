using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public static BossHealth Instance;

    public float health;
    public float maxHealth;
    public Slider healthSlider;
    public Animator animator;
    public GameObject keyGameObject;

    public GameObject bossHealthBar;

    private void Awake()
    {
        Instance = this;
    }

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

    public void ReceiveDamageGula(float damage)
    {
        if (GulaMov.instance.gulaState == GulaMov.GulaState.Iddle)
        {
            health -= damage * 2;

            if (health < 0)
            {
                DeathGula();
                bossHealthBar.SetActive(false);
            }
        }
        else
        {
            health -= damage;

            if (health < 0)
            {
                DeathGula();
                bossHealthBar.SetActive(false);
            }
        }

        if (health <= maxHealth / 2)
        {
            if (!GulaMov.instance.enraged) GulaMov.instance.enraged = true;
        }
    }

    public void ReceiveDamageCodicia(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            DeathCodicia();
            bossHealthBar.SetActive(false);
        }    
    }

    void DeathGula()
    {
        Instantiate(keyGameObject, new Vector3(GulaMov.instance.transform.position.x, GulaMov.instance.transform.position.y + 3f, GulaMov.instance.transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }

    void DeathCodicia()
    {
        Instantiate(keyGameObject, new Vector3(CodiciaMov.instance.transform.position.x, CodiciaMov.instance.transform.position.y -3f, CodiciaMov.instance.transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }

}
