using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public float duration = 1f;
    public float maxScale = 2f;
    public float speed = 1f;
    public float damage;
    public float pushForce;

    private Vector3 initialScale;
    private float elapsedTime = 0f;

    public GameObject specialAttackEffect; 

    private SpriteRenderer spriteRenderer;

    HashSet<Collider2D> hitEnemies = new HashSet<Collider2D>();

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialScale = transform.localScale;
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        GameObject specialAttackAnimation = Instantiate(specialAttackEffect, PlayerMov.instance.transform.position, Quaternion.identity);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            float scale = Mathf.Lerp(5, maxScale, progress);
            transform.localScale = initialScale * scale;
            DealDamage(scale);
            yield return null;
        }

        Destroy(specialAttackAnimation);
        Destroy(gameObject);
    }



    private void DealDamage(float scale)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, scale / 2f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                if (hitEnemies.Add(collider))
                {
                    if (collider.gameObject.GetComponent<EnemyHealth>() != null)
                    {
                        collider.gameObject.GetComponent<EnemyHealth>().ReceiveDamage(damage);
                    }

                    if (collider.gameObject.GetComponent<BossManager>() != null)
                    {
                        collider.gameObject.GetComponent<BossManager>().ReceiveDamage(damage);
                    }
                }
            }
        }
    }

}
