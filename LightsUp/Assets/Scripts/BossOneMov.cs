using System.Collections;
using UnityEngine;

public class BossOneMov : MonoBehaviour
{
    public float horizontalDistance = 5f; // Distancia horizontal del movimiento en "8"
    public float verticalDistance = 2f; // Distancia vertical del movimiento en "8"
    public float speed = 1f; // Velocidad del movimiento

    private float timeElapsed = 0f; // Tiempo transcurrido para el movimiento
    public bool infinito;
    public bool volviendo;


    [Header("Seguir al jugador")]
    public bool playerInRange = false; // Indica si el jugador est� en rango
    public float detectionRange = 6f; // Rango de visi�n del jefe
    public Transform player; // Referencia al jugador

    public LayerMask layerJugador;
    Vector3 spawnpoint;


    private void Start()
    {
        
        infinito = true;
    }
    void Update()
    {
        if(spawnpoint == Vector3.zero)
        {
            spawnpoint = transform.position;
        }

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, detectionRange, Vector2.zero, 0, layerJugador);

        playerInRange = hit.collider != null;
        if (!playerInRange  && !infinito)
        {
            Volver();

        }


        // Verificar si el jugador est� en rango
        if (playerInRange)
        {
            infinito = false;
            PursuePlayer(); // Persigue al jugador si est� en rango
        }
        if(infinito)
        {
            MoveInFigureEight(); // Movimiento en forma de "8" si el jugador no est� en rango
        }
    }

    void MoveInFigureEight()
    {
        // Incrementar el tiempo en funci�n de la velocidad
        timeElapsed += Time.deltaTime * speed;

        // Calcular la posici�n en forma de "8"
        float x = horizontalDistance * Mathf.Sin(timeElapsed); // Movimiento horizontal
        float y = verticalDistance * Mathf.Sin(2 * timeElapsed); // Movimiento vertical (doble frecuencia)

        // Actualizar la posici�n del enemigo
        transform.position = new Vector3(x, y, transform.position.z);
    }


    void Volver()
    {
        timeElapsed = 0;

        Vector3 directionToPlayer = (Vector3.zero - transform.position).normalized;


        transform.position = Vector3.MoveTowards(transform.position,Vector3.zero,speed*Time.deltaTime);

        if(transform.position== Vector3.zero)
        {
            infinito = true;
            timeElapsed = 0;

        }

    }



    void PursuePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;


        // Obtener la direcci�n hacia el jugador
        Vector3 directionToPlayer = (player.position - transform.position).normalized;


        // Actualizar la posici�n del jefe (con un ligero desplazamiento vertical para simular flotaci�n)
        transform.position += directionToPlayer * speed * Time.deltaTime;
       // transform.position = new Vector3(transform.position.x, transform.position.y + floatingOffset, transform.position.z);
    }

    // Opcional: Dibujar el rango de detecci�n en el editor
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Dibujar el rango de detecci�n
    }
}

