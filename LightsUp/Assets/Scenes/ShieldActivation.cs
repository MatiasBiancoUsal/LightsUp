using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldActivation : MonoBehaviour
{
    public GameObject shieldPrefab; // Prefab del escudo
    public Transform shieldSpawnPoint; // Punto de generaci�n del escudo
    public float shieldDuration = 5f; // Duraci�n del escudo
    private GameObject activeShield; // Referencia al escudo activo
    private int protection; // Cantidad de ataques que puede bloquear
    private bool shieldActive;

    void Update()
    {
        shieldActive = protection > 0;

        // Si hay protecci�n activa y no hay un escudo ya generado, crear uno
        if (shieldActive && activeShield == null)
        {
            ActivateShield();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            protection = 2; // Activar la protecci�n
            ActivateShield(); // Activar visualmente el escudo
            Destroy(other.gameObject); // Destruir el �tem del escudo despu�s de recogerlo
        }

        if (other.CompareTag("Enemy") && shieldActive)
        {
            protection--; // Reducir la protecci�n al recibir un ataque

            // Si la protecci�n se agota, destruir el escudo activo
            if (protection <= 0 && activeShield != null)
            {
                Destroy(activeShield);
                activeShield = null; // Resetear la referencia al escudo
            }
        }
    }

    void ActivateShield()
    {
        // Crear el escudo en el punto de generaci�n si no existe ya uno activo
        if (activeShield == null)
        {
            activeShield = Instantiate(shieldPrefab, shieldSpawnPoint.position, shieldSpawnPoint.rotation);
            StartCoroutine(DeactivateShieldAfterTime(shieldDuration)); // Desactivar despu�s de un tiempo
        }
    }

    IEnumerator DeactivateShieldAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (activeShield != null)
        {
            Destroy(activeShield); // Desactivar y destruir el escudo despu�s de la duraci�n
            protection = 0; // Resetear la protecci�n
            activeShield = null; // Resetear la referencia al escudo
        }
    }

    // Visualizar el rango de detecci�n en la escena
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
