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
    public int damageToPlayer = 1;
    public float damageInterval = 1f;
    private float damageTimer = 0f;
    private bool isGrabbingPlayer = false;
    private bool playerIsNear = false;

    public float grabbingDuration = 2f;
    private float grabbingTimer = 0f;

    public float waitTime = 2f;
    public bool WaitInPlace;

    public float retreatDistance = 1f;

    private Vector3 spawnpoint;
    private GameObject Enemigo;
    private bool MirarIzquierda;

    private Rigidbody2D playerRb;
    private Animator bossAnimator;

    public bool isAttacked = false;

    private bool playerHasMoved = false;
    private bool playerIsGrabbed = false;

    public float escapeTimeLimit = 1f;
    private float escapeTimer = 0f;
    private bool playerEscaped = false;

    // Nueva variable para el sistema de escape
    public int requiredEscapes = 5;  // Veces que se debe presionar la barra espaciadora para liberarse.
    private int currentEscapes = 0;  // Contador de cuántas veces se ha presionado la barra espaciadora.


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody2D>();
        }

        spawnpoint = transform.position;
        Enemigo = gameObject;

        bossAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            playerInRange = Vector2.Distance(transform.position, player.position) <= detectionRange;
        }

        if (playerInRange && !isGrabbingPlayer && !isAttacked)
        {
            infinito = false;
            volviendo = true;
            PursuePlayer();

            MirarIzquierda = player.position.x > Enemigo.transform.position.x;

            if (Vector3.Distance(transform.position, player.position) < stopDistance && !isGrabbingPlayer && !WaitInPlace)
            {
                StartCoroutine(GrabPlayer());
            }
        }
        else if (!playerInRange && !isGrabbingPlayer)
        {
            if (!playerIsNear)
            {
                MoveInFigureEight();
            }
        }
        else
        {
            if (playerIsNear && !isGrabbingPlayer)
            {
                PursuePlayer();
            }
        }

        Rotar();
        damageTimer -= Time.deltaTime;

        if (isGrabbingPlayer)
        {
            grabbingTimer -= Time.deltaTime;
            escapeTimer += Time.deltaTime;

            // Detectar presiones de la barra espaciadora
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentEscapes++;
                if (currentEscapes >= requiredEscapes) // Si el jugador presiona lo suficiente para escapar
                {
                    playerEscaped = true;
                    ReleasePlayer();
                }
            }

            // Si se acaba el tiempo sin haber escapado
            if (grabbingTimer <= 0 && !playerEscaped)
            {
                ApplyDamageToPlayer();
                ReleasePlayer();
            }
        }


        if(WaitInPlace)
        {
            transform.position = transform.position;
        }
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

    void PursuePlayer()
    {
        if (!isGrabbingPlayer && player != null)
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
        if (bossAnimator != null)
        {
            bossAnimator.SetTrigger("GrabPlayer");
        }

        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        isGrabbingPlayer = true;
        grabbingTimer = grabbingDuration;
        playerIsGrabbed = true;
        playerEscaped = false;
        escapeTimer = 0f;
        currentEscapes = 0;  // Reiniciar el contador de presiones de la barra espaciadora.

        playerIsNear = true;

        yield return new WaitForSeconds(grabbingDuration);

        ReleasePlayer();
    }

    public void ReleasePlayer()
    {
        isGrabbingPlayer = false;
        playerIsGrabbed = false;

        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.None;
        }

        StartCoroutine(ResetMovement());
    }

    private IEnumerator ResetMovement()
    {
        WaitInPlace = true;
        yield return new WaitForSeconds(waitTime);
        WaitInPlace = false;

    }

    private void ApplyDamageToPlayer()
    {
        PlayerHealth playerHealth = player?.GetComponent<PlayerHealth>();
        if (playerHealth != null && damageTimer <= 0)
        {
            playerHealth.ReceiveDamage(damageToPlayer, this.gameObject.name);
            damageTimer = damageInterval;
        }
    }

    public void ReceiveDamage()
    {
        isAttacked = true;
        infinito = false;
        volviendo = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}



























