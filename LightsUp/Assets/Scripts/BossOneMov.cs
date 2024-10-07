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
    public bool playerInRange = false; // Indica si el jugador est� en rango
    public float detectionRange = 6f; // Rango de visi�n del jefe
    public Transform player; // Referencia al jugador

    public LayerMask layerJugador;
    private Vector3 spawnpoint; // Declarar spawnpoint

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Asigna al jugador al inicio
        spawnpoint = transform.position; // Inicializa spawnpoint a la posici�n inicial del jefe
    }

    void Update()
    {
        // Verificar si el jugador est� en rango
        playerInRange = Physics2D.CircleCast(transform.position, detectionRange, Vector2.zero, 0, layerJugador).collider != null;

        if (playerInRange)
        {
            infinito = false; // Deja de moverse en "8" para perseguir al jugador
            PursuePlayer(); // Persigue al jugador si est� en rango
        }
        else
        {
            if (!volviendo)
            {
                MoveInFigureEight(); // Movimiento en forma de "8" si el jugador no est� en rango
            }
            else
            {
                Volver(); // L�gica para volver a un punto inicial
            }
        }
    }

    void MoveInFigureEight()
    {
        // Incrementar el tiempo en funci�n de la velocidad
        timeElapsed += Time.deltaTime * speed;

        // Calcular la posici�n en forma de "8"
        float x = horizontalDistance * Mathf.Sin(timeElapsed); // Movimiento horizontal
        float y = verticalDistance * Mathf.Sin(2 * timeElapsed); // Movimiento vertical (doble frecuencia)

        // Actualizar la posici�n del enemigo, manteniendo la altura constante
        transform.position = new Vector3(x, y + spawnpoint.y, transform.position.z);
    }

    void Volver()
    {
        // Implementar l�gica para volver a la posici�n original
        Vector3 directionToSpawn = spawnpoint - transform.position; // Supongamos que spawnpoint es la posici�n inicial
        transform.position += directionToSpawn.normalized * speed * Time.deltaTime;

        // Si est� cerca de la posici�n inicial, cambiar el estado
        if (Vector3.Distance(transform.position, spawnpoint) < 0.1f)
        {
            volviendo = false; // Regresar a movimiento en "8"
            timeElapsed = 0;
            infinito = true; // Regresar al movimiento en "8"
        }
    }

    void PursuePlayer()
    {
        // Obtener la direcci�n hacia el jugador
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Actualizar la posici�n del jefe (con un ligero desplazamiento vertical para simular flotaci�n)
        // Mantener la altura constante
        transform.position += new Vector3(directionToPlayer.x, 0, 0) * speed * Time.deltaTime; // Solo mover en horizontal
        transform.position = new Vector3(transform.position.x, spawnpoint.y, transform.position.z); // Mantener la altura
    }

    // Opcional: Dibujar el rango de detecci�n en el editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Dibujar el rango de detecci�n
    }
}





