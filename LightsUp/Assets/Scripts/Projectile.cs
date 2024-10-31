using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;
    public float speed = 5f;

    private void Start()
    {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.right * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Verifica si el jugador está vivo
            if (PlayerHealth.instance.state == PlayerHealth.PlayerStates.Alive)
            {
                PlayerHealth.instance.ReceiveDamage(damage, this.gameObject.name); 
            }

            Destroy(gameObject); 
        }
    }
}

