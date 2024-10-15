using UnityEngine;

public class SubditoController : MonoBehaviour
{
    public float speed = 3f;
    public int damage = 1;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.1f;

    private Transform player;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGround();

        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        }

        if (!isGrounded && rb.velocity.y < -10f)
        {
            rb.velocity = new Vector2(rb.velocity.x, -10f);
        }
    }
    
        public void SetPlayer(Transform newPlayer)
        {
            player = newPlayer;
        }
        public class MinionHealth : MonoBehaviour
        {
            public float health = 10f;

            public void ReceiveDamage(float damage)
            {
                health -= damage;
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        } 
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
      

    private void CheckGround()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

}
