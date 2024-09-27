using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricAttack : MonoBehaviour
{
    public int maxUses = 2; // Cantidad máxima de veces que se puede usar el ataque
    public float attackRadius = 5f; // Radio de ataque en unidades
    public int attackDamage = 50; // Daño que se inflige a los enemigos
    public float rayDuration = 0.1f; // Duración del efecto de rayo

    private int remainingUses;

    void Start()
    {
        remainingUses = maxUses; // Inicializa la cantidad de usos restantes
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && remainingUses > 0)
        {
            ExecuteAttack();
            remainingUses--; // Disminuye el contador de usos restantes
        }
    }

    void ExecuteAttack()
    {
        Vector2 attackStartPosition = (Vector2)transform.position; // Posición inicial
        Vector2 rightDirection = (Vector2)transform.right;

        // Crear rayos hacia adelante (derecha)
        CreateForwardRays(attackStartPosition, rightDirection);

        // Crear rayos hacia atrás (izquierda)
        CreateBackwardRays(attackStartPosition, rightDirection);

        // Inflige daño (solo para prueba)
        Debug.Log("Electric attack executed! Damage dealt: " + attackDamage + " to imaginary enemies.");
    }

    void CreateForwardRays(Vector2 startPosition, Vector2 direction)
    {
        Vector2 straightRightEndPosition = startPosition + direction * attackRadius; // Rayo recto hacia adelante
        Vector2 upwardRightEndPosition = startPosition + (direction + Vector2.up * 0.5f) * attackRadius; // Rayo inclinado hacia arriba
        Vector2 downwardRightEndPosition = startPosition + (direction - Vector2.up * 0.5f) * attackRadius; // Rayo inclinado hacia abajo

        // Muestra los rayos hacia adelante
        StartCoroutine(ShowElectricEffects(startPosition, straightRightEndPosition, upwardRightEndPosition, downwardRightEndPosition));
    }

    void CreateBackwardRays(Vector2 startPosition, Vector2 direction)
    {
        Vector2 backwardDirection = -direction; // Dirección hacia atrás
        Vector2 straightLeftEndPosition = startPosition + backwardDirection * attackRadius; // Rayo recto hacia atrás
        Vector2 upwardLeftEndPosition = startPosition + (backwardDirection + Vector2.up * 0.5f) * attackRadius; // Rayo inclinado hacia arriba hacia atrás
        Vector2 downwardLeftEndPosition = startPosition + (backwardDirection - Vector2.up * 0.5f) * attackRadius; // Rayo inclinado hacia abajo hacia atrás

        // Muestra los rayos hacia atrás
        StartCoroutine(ShowElectricEffects(startPosition, straightLeftEndPosition, upwardLeftEndPosition, downwardLeftEndPosition));
    }

    IEnumerator ShowElectricEffects(Vector2 startPosition, Vector2 endPosition1, Vector2 endPosition2, Vector2 endPosition3)
    {
        GameObject ray1 = CreateElectricRay(startPosition, endPosition1);
        GameObject ray2 = CreateElectricRay(startPosition, endPosition2);
        GameObject ray3 = CreateElectricRay(startPosition, endPosition3);

        // Espera la duración del rayo antes de destruir
        yield return new WaitForSeconds(rayDuration);

        // Destruir los rayos
        Destroy(ray1);
        Destroy(ray2);
        Destroy(ray3);
    }

    GameObject CreateElectricRay(Vector2 startPosition, Vector2 endPosition)
    {
        GameObject rayObject = new GameObject("ElectricRay");
        LineRenderer lineRenderer = rayObject.AddComponent<LineRenderer>();

        // Configura el LineRenderer
        lineRenderer.startWidth = 0.1f; // Ancho inicial del rayo
        lineRenderer.endWidth = 0.1f; // Ancho final del rayo
        lineRenderer.positionCount = 2; // Dos puntos para el rayo
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Material básico
        lineRenderer.startColor = Color.white; // Color inicial del rayo (blanco)
        lineRenderer.endColor = Color.white; // Color final del rayo (blanco)

        // Establece los puntos del rayo
        lineRenderer.SetPosition(0, startPosition); // Posición inicial
        lineRenderer.SetPosition(1, endPosition); // Posición final

        return rayObject; // Devuelve el objeto del rayo creado
    }

    // Visualizar el radio de ataque en la escena
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}








