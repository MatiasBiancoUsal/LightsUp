using UnityEngine;

public class BalancePlatform : MonoBehaviour
{
    public Rigidbody2D platformRigidbody;   // Rigidbody de la plataforma
    public Transform playerTransform;       // Transform del jugador
    public float balanceForce = 10f;        // Fuerza de balanceo para aplicar torque
    public float maxRotation = 30f;         // Grados m�ximos que puede rotar la plataforma

    private void Start()
    {
        platformRigidbody.freezeRotation = false; // Permite que la plataforma rote
    }

    void Update()
    {

        if (IsPlayerOnPlatform())  // Funci�n que detecta si el jugador est� en la plataforma
        {
            ApplyBalance();  // Aplica torque a la plataforma dependiendo de la posici�n del jugador
        }
    }

    // Comprueba si el jugador est� en la plataforma
    bool IsPlayerOnPlatform()
    {
        
        float distance = Vector2.Distance(playerTransform.position, transform.position);
        float platformWidth = GetComponent<Collider2D>().bounds.size.x;

        return distance < platformWidth / 2f; // Asume que el jugador est� en la plataforma si est� dentro del ancho de la plataforma
    }

    // Aplica torque a la plataforma dependiendo de la posici�n del jugador
    void ApplyBalance()
    {
        
        float playerRelativePosition = playerTransform.position.x - transform.position.x;

        
        float appliedTorque = playerRelativePosition * balanceForce;

        // Limitar la rotaci�n de la plataforma
        if (platformRigidbody.rotation > maxRotation)
        {
            platformRigidbody.rotation = maxRotation;
        }
        else if (platformRigidbody.rotation < -maxRotation)
        {
            platformRigidbody.rotation = -maxRotation;
        }
        else
        {
            // Aplicar el torque para hacer que la plataforma se incline
            platformRigidbody.AddTorque(-appliedTorque);
        }
    }
}




