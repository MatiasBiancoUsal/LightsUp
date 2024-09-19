using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 5f; 
    public float chaseRange = 3f;      
    public float speed = 2f;            
    public Transform player;            
    public Animator animator;           
    public float groundYLevel;          

    private Vector2 startPosition;      
    private bool isPlayerDetected = false;

    void Start()
    {
        startPosition = transform.position;
        animator.SetTrigger("ToIdle");
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            isPlayerDetected = true;

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                animator.SetTrigger("ToWalk");
            }
            FollowPlayer();
        }
        else
        {
            ReturnToStart();
        }

        KeepOnGround();
    }

    void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void ReturnToStart()
    {
        Vector2 direction = (startPosition - (Vector2)transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        if (Vector2.Distance(transform.position, startPosition) < 0.1f)
        {
            transform.position = startPosition; 
            animator.SetTrigger("ToIdle");
        }
    }

    void KeepOnGround()
    {
        Vector3 position = transform.position;
        position.y = groundYLevel; 
        transform.position = position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            
            animator.SetTrigger("ToAttack");

            Invoke("ReloadScene", 1f); 
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}
