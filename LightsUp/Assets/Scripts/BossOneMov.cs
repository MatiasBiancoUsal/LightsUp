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
    public int damageToPlayer = 1;  // Da�o por cada agarre
    public float damageInterval = 1f;  // Intervalo de da�o
    private float damageTimer = 0f;
    private bool isGrabbingPlayer = false;
    private bool playerIsNear = false;  // Para saber si el jugador est� cerca despu�s de soltarse

    // Nuevo valor para controlar la duraci�n del agarre
    public float grabbingDuration = 2f;
    private float grabbingTimer = 0f; // Temporizador para el agarre

    public float waitTime = 2f;
    public float retreatDistance = 1f;

    private Vector3 spawnpoint;
    private GameObject Enemigo;
    private bool MirarIzquierda;

    private Rigidbody2D playerRb; // Referencia al Rigidbody2D del jugador
    private Animator bossAnimator; // Referencia al Animator del boss

    // Variable para controlar si el boss ha sido atacado
    public bool isAttacked = false;

    // Variables para controlar si el jugador se mueve
    private bool playerHasMoved = false;
    private bool playerIsGrabbed = false; // Para saber si el jugador ya ha sido agarrado

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

        if (playerInRange && !isGrabbingPlayer && !isAttacked) // Solo persigue si no est� agarrando al jugador ni ha sido atacado
        {
            infinito = false;
            volviendo = true;
            PursuePlayer();

            // Actualizar MirarIzquierda seg�n la posici�n relativa del jugador
            MirarIzquierda = player.position.x > Enemigo.transform.position.x;

            if (Vector3.Distance(transform.position, player.position) < stopDistance && !isGrabbingPlayer)
            {
                StartCoroutine(GrabPlayer());
            }
        }
        else if (!playerInRange && !isGrabbingPlayer)
        {
            if (!playerIsNear) // Si el jugador se alej� completamente
            {
                MoveInFigureEight();
            }
        }
        else
        {
            // Si el jugador ya no est� cerca y no ha sido atacado
            if (playerIsNear && !isGrabbingPlayer)
            {
                // El jugador est� en rango despu�s de haberse alejado, vuelve a perseguirlo
                PursuePlayer();
            }
        }

        Rotar(); // Asegurar que el boss rota en la direcci�n correcta
        damageTimer -= Time.deltaTime;

        // Si el boss est� agarrando al jugador, maneja el tiempo de agarre
        if (isGrabbingPlayer)
        {
            grabbingTimer -= Time.deltaTime;
            if (grabbingTimer <= 0)
            {
                ReleasePlayer();
            }
            else
            {
                ApplyDamageToPlayer();
            }
        }
    }

    void Rotar()
    {
        // Rota el boss seg�n la direcci�n en la que debe mirar
        Enemigo.transform.rotation = MirarIzquierda ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.identity;
    }

    void MoveInFigureEight()
    {
        timeElapsed += Time.deltaTime * speed;
        float x = horizontalDistance * Mathf.Sin(timeElapsed);
        float y = verticalDistance * Mathf.Sin(2 * timeElapsed);
        transform.position = new Vector3(x, y + spawnpoint.y, transform.position.z);
    }

    void PursuePlayer()
    {
        if (!isGrabbingPlayer && player != null) // Solo lo persigue si no lo est� agarrando
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

    private IEnumerator GrabPlayer()
    {
        // Inicia la animaci�n de agarre (opcional)
        if (bossAnimator != null)
        {
            bossAnimator.SetTrigger("GrabPlayer");
        }

        // Congela al jugador
        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        isGrabbingPlayer = true;
        grabbingTimer = grabbingDuration; // Inicia el temporizador de agarre
        playerIsGrabbed = true;  // Marca al jugador como agarrado

        // Marcar que el jugador est� cerca del boss y que el boss deber�a seguirlo
        playerIsNear = true;

        yield return new WaitForSeconds(grabbingDuration); // Mantener al jugador agarrado por un tiempo

        // Liberar al jugador despu�s de la duraci�n
        ReleasePlayer();
    }

    // Esta funci�n ser� llamada desde un evento de la animaci�n
    public void ReleasePlayer()
    {
        // Liberar al jugador cuando el boss lo suelta
        isGrabbingPlayer = false;
        playerIsGrabbed = false;

        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.None; // Libera al jugador
        }

        // Restaurar el movimiento del boss
        StartCoroutine(ResetMovement());
    }

    private IEnumerator ResetMovement()
    {
        // Espera un momento para asegurarse de que la animaci�n y el soltar est�n completos
        yield return new WaitForSeconds(0.5f);
        infinito = true;   // Activar el movimiento en infinito
        volviendo = false; // El Boss ya no est� en modo de volver
        timeElapsed = 0;   // Restablecer el tiempo de movimiento

        // Una vez que el jugador se ha alejado, cambiar el estado del jugador
        playerIsNear = false;
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

    // Nueva funci�n para aplicar el da�o y marcar que el boss fue atacado
    public void ReceiveDamage()
    {
        isAttacked = true;
        // Detenemos el movimiento cuando el boss es atacado
        infinito = false;
        volviendo = false;
        // Aqu� puedes agregar efectos visuales o sonidos
        // Si necesitas hacer alguna acci�n especial despu�s del da�o, agr�gala aqu�.
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}























