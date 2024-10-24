using UnityEngine;

public class TreeTrunk : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public float detectionDistance = 10f; // Distancia de detección
    public Transform player; // Referencia al objeto del jugador

    private void Update()
    {
        // Verificar la distancia entre el tronco y el jugador
        if (Vector3.Distance(transform.position, player.position) <= detectionDistance)
        {
            MoveLeft();
        }
    }

    private void MoveLeft()
    {
        // Mover hacia la izquierda solo en el eje X
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
}


