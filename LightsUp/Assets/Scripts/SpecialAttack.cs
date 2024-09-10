using System.Collections;
using System.Collections.Generic;
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
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            float scale = Mathf.Lerp(5, maxScale, progress);
            transform.localScale = initialScale * scale;
            DealDamage();
            yield return null;
        }

        Destroy(gameObject);

    }

    private void DealDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, maxScale / 2f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                if (hitEnemies.Add(collider))
                {
                    Debug.Log("daño");
                    collider.gameObject.GetComponent<EnemyHealth>().ReceiveDamage(damage);
                    Rigidbody2D rb2d = collider.GetComponent<Rigidbody2D>();
                    if (rb2d != null)
                    {
                        Vector2 direction = (collider.transform.position - transform.position).normalized;
                        rb2d.AddForce(direction * pushForce, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }

}
