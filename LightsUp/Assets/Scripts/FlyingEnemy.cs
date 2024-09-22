using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed = 3f; // Velocidad de movimiento del enemigo
    public float detectionRange = 10f; // Rango en el que el enemigo detecta al jugador
    public float attackRange = 1f; // Rango en el que el enemigo puede atacar al jugador
    public float damage = 10f; // Daño que el enemigo daña al jugador
    public float minHeight = 1.5f; // Altura mínima en la que el enemigo vuela (en relación al jugador)
    public float attackCooldown = 2f; // Tiempo de espera después de atacar
    public float passiveHeight = 5f; // Altura a la que vuela pasivamente cuando la luz está apagada
    public float knockbackDistance = 2f; // Distancia que retrocede el enemigo al ser impactado con la luz
    public float knockbackSpeed = 5f; // Velocidad a la que retrocede el enemigo
    public float patrolWidth = 5f; // Ancho del área en la que el enemigo patrulla
    public float patrolSpeed = 2f; // Velocidad a la que el enemigo patrulla de lado a lado

    private Transform player; // Referencia al jugador
    public bool isWaitingAfterAttack = false; // Para saber si el enemigo está esperando después de atacar
    private bool isKnockbackActive = false; // Para saber si el retroceso está activo
    private bool isPatrolling = false; // Para saber si el enemigo está patrullando

    // Referencia al script FlashlightManager
    private FlashlightManager flashlightManager;

    // Referencia al script LifeCaracter
    private LifeCaracter playerLife;

    // Componente SpriteRenderer del enemigo
    private SpriteRenderer spriteRenderer;

    private Vector3 patrolStartPosition; // Posición inicial para patrullar

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

        // Obtiene el componente SpriteRenderer del enemigo
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Establece la posición de inicio para patrullar
        patrolStartPosition = transform.position;
    }

    private void Update()
    {
        // Verifica si el script FlashlightManager y el jugador existen
        if (flashlightManager == null || player == null || playerLife == null)
            return;

        // Si el retroceso está activo, continúa retrocediendo
        if (isKnockbackActive)
        {
            Knockback();
            return;
        }

        // Verifica si el jugador tiene la linterna encendida
        if (flashlightManager.isFlashlightOn && flashlightManager.flashlightEnergy > 0)
        {
            // Si la linterna está encendida, el enemigo sigue al jugador
            isPatrolling = false; // Detiene la patrulla
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < detectionRange && !isWaitingAfterAttack)
            {
                MoveTowardsPlayer();
            }
        }
        else
        {
            // Si la linterna está apagada, el enemigo patrulla
            isPatrolling = true; // Activa la patrulla
            Patrol();
        }
    }

    private void Patrol()
    {
        // Calcula la dirección de patrulla (de izquierda a derecha)
        float patrolDirection = Mathf.PingPong(Time.time * patrolSpeed, patrolWidth) - (patrolWidth / 2f);
        Vector3 patrolPosition = new Vector3(patrolStartPosition.x + patrolDirection, player.position.y + passiveHeight, transform.position.z);
        transform.position = Vector2.MoveTowards(transform.position, patrolPosition, speed * Time.deltaTime);

        // Actualiza la dirección del sprite del enemigo mientras patrulla
        UpdateSpriteDirection(Vector2.right * Mathf.Sign(patrolDirection));
    }

    private void FlyPassively()
    {
        // El enemigo vuela a una altura constante (passiveHeight)
        Vector3 passivePosition = new Vector3(transform.position.x, player.position.y + passiveHeight, transform.position.z);
        transform.position = Vector2.MoveTowards(transform.position, passivePosition, speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D objeto)
    {
        if (objeto.gameObject.tag == "Player")
        {

                StartCoroutine("ResetAttack");

        }
    }

    private void MoveTowardsPlayer()
    {
        // Obtén la posición del jugador
        Vector3 playerPosition = player.position;

        // Asegúrate de que el enemigo no descienda más allá de minHeight respecto al jugador
        if (transform.position.y < playerPosition.y + minHeight)
        {
            playerPosition.y = transform.position.y;  // Mantén la altura del enemigo a un nivel constante si está por debajo de minHeight
        }

        // Mueve el enemigo hacia el jugador, pero respetando la altura mínima
        Vector2 direction = (playerPosition - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);

        // Actualiza la dirección del sprite del enemigo
        UpdateSpriteDirection(direction);
    }

    private void Knockback()
    {
        // Calcula la dirección de retroceso (en dirección opuesta al jugador)
        Vector2 knockbackDirection = (transform.position - player.position).normalized;

        // Mueve al enemigo hacia atrás en la dirección actualizada del jugador
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)knockbackDirection, knockbackSpeed * Time.deltaTime);

        // Desactiva el retroceso después de retroceder cierta distancia
        if (Vector2.Distance(transform.position, player.position) >= knockbackDistance)
        {
            isKnockbackActive = false;
            StartCoroutine("ResetAttack"); // Después del retroceso, comienza el cooldown del ataque
        }

        // Actualiza la dirección del sprite del enemigo mientras retrocede
        UpdateSpriteDirection(knockbackDirection);
    }

    private void UpdateSpriteDirection(Vector2 direction)
    {
        // Si el enemigo se mueve a la izquierda, voltea el sprite
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        // Si el enemigo se mueve a la derecha, no voltea el sprite
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private IEnumerator ResetAttack()
    {
        // Espera un tiempo antes de permitir otro ataque
        isKnockbackActive = true;
        isWaitingAfterAttack = true;  // Marca que está esperando
        print("choco con jugador y espera cooldown");
        yield return new WaitForSeconds(attackCooldown); // Tiempo de espera entre ataques
        isWaitingAfterAttack = false; // El enemigo puede seguir al jugador de nuevo
        isKnockbackActive = false;
    }
}




