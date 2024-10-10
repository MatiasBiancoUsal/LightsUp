using System.Collections;
using UnityEngine;

public class BossOneMov : MonoBehaviour
{
    public float horizontalDistance = 5f; // Distancia horizontal del movimiento en "8"
    public float verticalDistance = 2f; // Distancia vertical del movimiento en "8"
    public float speed = 1f; // Velocidad del movimiento

    private float timeElapsed = 0f; // Tiempo transcurrido para el movimiento
    public bool infinito = true; // Inicialmente en true
    public bool volviendo = false;

    [Header("Seguir al jugador")]
    public bool playerInRange = false; // Indica si el jugador está en rango
    public float detectionRange = 6f; // Rango de visión del jefe
    public Transform player; // Referencia al jugador

    public LayerMask layerJugador;
    public int damageToPlayer = 1; // Cantidad de daño que se le hace al jugador

    public float waitTime = 2f;

    private Vector3 spawnpoint; // Declarar spawnpoint
    public GameObject Enemigo;
    public bool MirarIzquierda;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Asigna al jugador al inicio
        spawnpoint = transform.position; // Inicializa spawnpoint a la posición inicial del jefe
        Enemigo = this.gameObject;
    }

    void Update()
    {
        // Verificar si el jugador está en rango
        playerInRange = Physics2D.CircleCast(transform.position, detectionRange, Vector2.zero, 0, layerJugador).collider != null;
        Rotar();
        if (playerInRange)
        {
            infinito = false; // Deja de moverse en "8" para perseguir al jugador
            volviendo = true;
            PursuePlayer(); // Persigue al jugador si está en rango
            if (player.position.x <= Enemigo.transform.position.x)
            {
                MirarIzquierda = true;
            }
            else
            {
                MirarIzquierda = false; 
            }

        }
        else
        {
            if (!volviendo)
            {
                MoveInFigureEight(); // Movimiento en forma de "8" si el jugador no está en rango

            }
            else
            {
                Volver(); // Lógica para volver a un punto inicial


            }
        }
    }

    void Rotar()
    {
        if (MirarIzquierda)
        {
            Enemigo.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        }
        else
        {
            Enemigo.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    void MoveInFigureEight()
    {
        // Incrementar el tiempo en función de la velocidad
        timeElapsed += Time.deltaTime * speed;

        // Calcular la posición en forma de "8"
        float x = horizontalDistance * Mathf.Sin(timeElapsed); // Movimiento horizontal
        float y = verticalDistance * Mathf.Sin(2 * timeElapsed); // Movimiento vertical (doble frecuencia)


        if (MirarIzquierda)
        {
            if(x<0 && y<0)
            {
                MirarIzquierda = false;
            }
        }
        else
        {
            if (x > 0 && y < 0)
            {
                MirarIzquierda = true;
            }
        }

        // Actualizar la posición del enemigo, manteniendo la altura constante
        transform.position = new Vector3(x, y + spawnpoint.y, transform.position.z);
    }

    void Volver()
    {
        if (Enemigo.transform.position.x > 0) {

            MirarIzquierda = true; 
        }

        else {

            MirarIzquierda = false;        
        }

        timeElapsed = 0;

        Vector3 directionToPlayer = (Vector3.zero - transform.position).normalized;

        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, speed * Time.deltaTime);

        if (transform.position == Vector3.zero)
        {
            StartCoroutine(ActivateReturnVariables()); // CORRECCIÓN: Se usa 'StartCoroutine'
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
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        transform.position += new Vector3(directionToPlayer.x, directionToPlayer.y, 0) * speed * Time.deltaTime;
    }

    // Detectar colisiones con el jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Acceder al script del jugador para restarle vida
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ReceiveDamage(damageToPlayer); // Llamar al método que reduce la vida del jugador
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}







