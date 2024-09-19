using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 5f; // Distancia para detectar al jugador
    public float chaseRange = 3f;      // Distancia para atrapar al jugador
    public float speed = 2f;            // Velocidad del enemigo
    public Transform player;            // Referencia al jugador
    public Animator animator;           // Componente Animator
    public float groundYLevel;          // Altura deseada en Y para el enemigo

    private Vector2 startPosition;      // Posición inicial del enemigo
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
            transform.position = startPosition; // Asegura que vuelva exactamente a la posición inicial
            animator.SetTrigger("ToIdle");
        }
    }

    void KeepOnGround()
    {
        Vector3 position = transform.position;
        position.y = groundYLevel; // Mantiene al enemigo en la altura deseada
        transform.position = position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Asegúrate de que el jugador tenga la etiqueta "Player"
        {
            // Activar la animación de ataque
            animator.SetTrigger("ToAttack");

            // Llama a la función de recarga de escena después de un tiempo
            Invoke("ReloadScene", 1f); // Ajusta el tiempo según la duración de la animación
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarga la escena actual
    }
}
