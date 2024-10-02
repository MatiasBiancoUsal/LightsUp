using UnityEngine;

public class BossOneMov : MonoBehaviour
{
    public float horizontalDistance = 5f; // Distancia horizontal del "8"
    public float verticalDistance = 2f; // Distancia vertical del "8"
    public float speed = 1f; // Velocidad del movimiento

    private float timeElapsed = 0f;

    void Update()
    {
        // Incrementar el tiempo en función de la velocidad
        timeElapsed += Time.deltaTime * speed;

        // Calcular la posición en forma de "8"
        float x = horizontalDistance * Mathf.Sin(timeElapsed); // Movimiento horizontal
        float y = verticalDistance * Mathf.Sin(2 * timeElapsed); // Movimiento vertical (doble frecuencia)

        // Actualizar la posición del enemigo
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
