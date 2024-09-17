using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed = 3f; // Velocidad de movimiento del enemigo
    public float detectionRange = 10f; // Rango en el que el enemigo detecta al jugador
    public float attackRange = 1f; // Rango en el que el enemigo puede atacar al jugador
    public float damage = 10f; // Daño que el enemigo inflige al jugador

    private Transform player; // Referencia al jugador
    private bool isAttacking = false; // Para saber si el enemigo está atacando

    // Referencia al script FlashlightManager
    private FlashlightManager flashlightManager;

    // Referencia al script LifeCaracter
    private LifeCaracter playerLife;

    private void Start()
    {
        // Encuentra al jugador en la escena
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Encuentra el componente FlashlightManager en la escena
        flashlightManager = FlashlightManager.instance;

        // Encuentra el componente LifeCaracter en el jugador
        if (player != null)
        {
            playerLife = player.GetComponent<LifeCaracter>();
        }
    }

    private void Update()
    {
        // Verifica si el script FlashlightManager y el jugador existen
        if (flashlightManager == null || player == null || playerLife == null)
            return;

        // Verifica si el jugador tiene la linterna encendida
        if (flashlightManager.flashlightEnergy > 0 && flashlightManager.isFlashlightOn)
        {
            // Calcula la distancia entre el enemigo y el jugador
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Si el jugador está dentro del rango de detección
            if (distanceToPlayer < detectionRange)
            {
                // Mueve el enemigo hacia el jugador
                MoveTowardsPlayer();

                // Si el enemigo está dentro del rango de ataque
                if (distanceToPlayer < attackRange)
                {
                    AttackPlayer();
                }
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        // Mueve el enemigo hacia el jugador
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void AttackPlayer()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            // Aquí se usa el método en el script LifeCaracter para recibir daño
            playerLife.ReceiveDamage(damage);
            StartCoroutine(ResetAttack());
        }
    }

    private IEnumerator ResetAttack()
    {
        // Espera un tiempo antes de permitir otro ataque
        yield return new WaitForSeconds(1f); // Ajusta el tiempo según sea necesario
        isAttacking = false;
    }
}


