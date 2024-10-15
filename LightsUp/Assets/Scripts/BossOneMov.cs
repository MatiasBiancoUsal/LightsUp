using System.Collections;
using UnityEngine;

public class BossOneMov : MonoBehaviour
{
    public float horizontalDistance = 5f;
    public float verticalDistance = 2f;
    public float speed = 1f;
    public float stopDistance = 0.1f;

    private float timeElapsed = 0f;
    public bool infinito = true;
    public bool volviendo = false;

    [Header("Seguir al jugador")]
    public bool playerInRange = false;
    public float detectionRange = 6f;
    public Transform player;

    public LayerMask layerJugador;
    public int damageToPlayer = 3;
    public float damageInterval = 1f;
    private float damageTimer = 0f;
    private bool isGrabbingPlayer = false;

    public float waitTime = 2f;
    public float retreatDistance = 1f;

    private Vector3 spawnpoint;
    private GameObject Enemigo;
    private bool MirarIzquierda;

    private Rigidbody2D playerRb; // Referencia al Rigidbody2D del jugador
    private Animator bossAnimator; // Referencia al Animator del boss

    // Añadimos esta variable para controlar si el boss ha sido atacado
    public bool isAttacked = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody2D>(); // Obtiene el Rigidbody2D del jugador
        }

        spawnpoint = transform.position;
        Enemigo = gameObject;

        // Obtener el Animator del boss
        bossAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            playerInRange = Vector2.Distance(transform.position, player.position) <= detectionRange;
        }

        Rotar();

        if (playerInRange && !isAttacked) // Solo sigue al jugador si no ha sido atacado
        {
            infinito = false;
            volviendo = true;
            PursuePlayer();

            MirarIzquierda = player.position.x <= Enemigo.transform.position.x;

            if (isGrabbingPlayer)
            {
                ApplyDamageToPlayer();
            }
        }
        else
        {
            if (!volviendo)
            {
                MoveInFigureEight();
            }
            else
            {
                Volver();
            }
        }

        damageTimer -= Time.deltaTime;
    }

    void Rotar()
    {
        Enemigo.transform.rotation = MirarIzquierda ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.identity;
    }

    void MoveInFigureEight()
    {
        timeElapsed += Time.deltaTime * speed;
        float x = horizontalDistance * Mathf.Sin(timeElapsed);
        float y = verticalDistance * Mathf.Sin(2 * timeElapsed);
        transform.position = new Vector3(x, y + spawnpoint.y, transform.position.z);
    }

    void Volver()
    {
        MirarIzquierda = Enemigo.transform.position.x > spawnpoint.x;
        timeElapsed = 0;
        transform.position = Vector3.MoveTowards(transform.position, spawnpoint, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, spawnpoint) < stopDistance)
        {
            StartCoroutine(ActivateReturnVariables());
        }
    }

    private IEnumerator ActivateReturnVariables()
    {
        yield return new WaitForSeconds(waitTime);
        infinito = true;
        volviendo = false;
        timeElapsed = 0;
    }

    void PursuePlayer()
    {
        if (!isGrabbingPlayer && player != null) // Solo lo persigue si no lo está agarrando
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.position += directionToPlayer * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, player.position) < stopDistance)
            {
                speed = Mathf.Lerp(speed, 0, Time.deltaTime * 5f);
            }
            else
            {
                speed = 1f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Usamos OnTriggerEnter2D para capturar el primer contacto
    {
        if (collision.CompareTag("Player"))
        {
            // Solo lo agarra si no está siendo agarrado ya
            if (!isGrabbingPlayer)
            {
                isGrabbingPlayer = true;

                // Iniciar la animación de agarre
                if (bossAnimator != null)
                {
                    bossAnimator.SetTrigger("Player"); // Aquí se puede activar una animación de "Player"
                }

                // Inmovilizar al jugador
                if (playerRb != null)
                {
                    playerRb.constraints = RigidbodyConstraints2D.FreezeAll; // Congela al jugador
                }

                // Aplicar daño de inmediato
                ApplyDamageToPlayer();
            }
        }

        // Verificar si el objeto con el nombre "SpecialAttack" colisiona
        if (collision.CompareTag("SpecialAttack"))
        {
            isGrabbingPlayer = false; 
            ReleasePlayer(); 
        }
    }

    // Esta función será llamada desde un evento de la animación
    public void ReleasePlayer()
    {
        // Liberar al jugador cuando el boss lo suelta
        isGrabbingPlayer = false;

        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.None; // Libera al jugador
        }

        // Restaurar el movimiento del boss
        StartCoroutine(ResetMovement());
    }

    private IEnumerator ResetMovement()
    {
        // Espera un momento para asegurarse de que la animación y el soltar estén completos
        yield return new WaitForSeconds(0.5f);
        infinito = true;   // Activar el movimiento en infinito
        volviendo = false; // El Boss ya no está en modo de volver
        timeElapsed = 0;   // Restablecer el tiempo de movimiento
    }

    private void ApplyDamageToPlayer()
    {
        PlayerHealth playerHealth = player?.GetComponent<PlayerHealth>();
        if (playerHealth != null && damageTimer <= 0)
        {
            playerHealth.ReceiveDamage(damageToPlayer);
            damageTimer = damageInterval;
        }
    }

    // Nueva función para aplicar el daño y marcar que el boss fue atacado
    public void ReceiveDamage()
    {
        isAttacked = true;
        // Detenemos el movimiento cuando el boss es atacado
        infinito = false;
        volviendo = false;
        // Aquí puedes agregar efectos visuales o sonidos
        // Si necesitas hacer alguna acción especial después del daño, agrégala aquí.
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}















