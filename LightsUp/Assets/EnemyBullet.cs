using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed;
    public int damage;
    private Vector2 bulletDirection;

    private void Update()
    {
        transform.Translate(bulletDirection * bulletSpeed * Time.deltaTime);
    }

    // Método para establecer la dirección
    public void SetDirection(Vector2 direction)
    {
        bulletDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }
}
