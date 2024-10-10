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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        spawnpoint = transform.position;
        Enemigo = gameObject;
    }

    void Update()
    {
        if (player != null)
        {
            playerInRange = Vector2.Distance(transform.position, player.position) <= detectionRange;
        }

        Rotar();

        if (playerInRange)
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
        if (player != null)
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isGrabbingPlayer)
            {
                isGrabbingPlayer = true;
                StartCoroutine(RetreatFromPlayer());
            }
        }
    }

    private IEnumerator RetreatFromPlayer()
    {
        Vector3 retreatDirection = (transform.position - player.position).normalized;
        Vector3 retreatPosition = transform.position + retreatDirection * retreatDistance;
        float retreatTime = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < retreatTime)
        {
            transform.position = Vector3.Lerp(transform.position, retreatPosition, (elapsedTime / retreatTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isGrabbingPlayer = false; 
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isGrabbingPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}








