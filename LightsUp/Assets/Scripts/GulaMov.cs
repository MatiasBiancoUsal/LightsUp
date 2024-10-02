using UnityEngine;

public class GulaMov : MonoBehaviour
{
    public float speed = 2f; // Velocidad normal
    public float slowSpeed = 0.5f; // Velocidad reducida
    public float range = 5f; // Rango de movimiento
    public float totalTime = 6f; // Tiempo total de movimiento

    private Vector3 startPosition;
    private float timeElapsed = 0f;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        // Calcular la velocidad según el tiempo transcurrido
        float currentSpeed = speed;
        if (timeElapsed > 3f && timeElapsed <= 5f) // Desde 3 segundos hasta 5 segundos
        {
            currentSpeed = slowSpeed;
        }

        // Mover al enemigo
        if (timeElapsed <= totalTime)
        {
            Vector3 movementDirection = movingRight ? Vector3.right : Vector3.left;
            transform.position += movementDirection * currentSpeed * Time.deltaTime;

            // Verificar límites de movimiento
            if (transform.position.x >= startPosition.x + range)
            {
                movingRight = false;
            }
            else if (transform.position.x <= startPosition.x - range)
            {
                movingRight = true;
            }
        }

        // Detener al enemigo después de 5 segundos
        if (timeElapsed > 5f)
        {
            transform.position = transform.position; // Mantener la posición
            if (timeElapsed >= totalTime)
            {
                timeElapsed = 0f; // Reiniciar el tiempo
            }
        }
    }
}
