using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldActivation : MonoBehaviour
{
    public GameObject shieldPrefab; // Prefab del escudo
    public Transform shieldSpawnPoint; // Punto de generación del escudo
    public float shieldDuration = 5f; // Duración del escudo
    private GameObject activeShield; // Referencia al escudo activo
    private int protection; // Cantidad de ataques que puede bloquear
    private bool shieldActive;

    void Update()
    {
        shieldActive = protection > 0;

        // Si hay protección activa y no hay un escudo ya generado, crear uno
        if (shieldActive && activeShield == null)
        {
            ActivateShield();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            protection = 2; // Activar la protección
            ActivateShield(); // Activar visualmente el escudo
            Destroy(other.gameObject); // Destruir el ítem del escudo después de recogerlo
        }

        if (other.CompareTag("Enemy") && shieldActive)
        {
            protection--; // Reducir la protección al recibir un ataque

            // Si la protección se agota, destruir el escudo activo
            if (protection <= 0 && activeShield != null)
            {
                Destroy(activeShield);
                activeShield = null; // Resetear la referencia al escudo
            }
        }
    }

    void ActivateShield()
    {
        // Crear el escudo en el punto de generación si no existe ya uno activo
        if (activeShield == null)
        {
            activeShield = Instantiate(shieldPrefab, shieldSpawnPoint.position, shieldSpawnPoint.rotation);
            StartCoroutine(DeactivateShieldAfterTime(shieldDuration)); // Desactivar después de un tiempo
        }
    }

    IEnumerator DeactivateShieldAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (activeShield != null)
        {
            Destroy(activeShield); // Desactivar y destruir el escudo después de la duración
            protection = 0; // Resetear la protección
            activeShield = null; // Resetear la referencia al escudo
        }
    }

    // Visualizar el rango de detección en la escena
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
