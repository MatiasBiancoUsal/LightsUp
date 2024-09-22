using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed = 3f; // Velocidad de movimiento del enemigo
    public float detectionRange = 10f; // Rango en el que el enemigo detecta al jugador
    public float attackRange = 1f; // Rango en el que el enemigo puede atacar al jugador
    public float damage = 10f; // Da�o que el enemigo da�a al jugador
    public float minHeight = 1.5f; // Altura m�nima en la que el enemigo vuela (en relaci�n al jugador)
    public float attackCooldown = 2f; // Tiempo de espera despu�s de atacar
    public float passiveHeight = 5f; // Altura a la que vuela pasivamente cuando la luz est� apagada
    public float knockbackDistance = 2f; // Distancia que retrocede el enemigo al ser impactado con la luz
    public float knockbackSpeed = 5f; // Velocidad a la que retrocede el enemigo
    public float patrolWidth = 5f; // Ancho del �rea en la que el enemigo patrulla
    public float patrolSpeed = 2f; // Velocidad a la que el enemigo patrulla de lado a lado

    private Transform player; // Referencia al jugador
    public bool isWaitingAfterAttack = false; // Para saber si el enemigo est� esperando despu�s de atacar
    private bool isKnockbackActive = false; // Para saber si el retroceso est� activo
    private bool isPatrolling = false; // Para saber si el enemigo est� patrullando

    // Referencia al script FlashlightManager
    private FlashlightManager flashlightManager;

    // Referencia al script LifeCaracter
    private LifeCaracter playerLife;

    // Componente SpriteRenderer del enemigo
    private SpriteRenderer spriteRenderer;

    private Vector3 patrolStartPosition; // Posici�n inicial para patrullar

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

        // Establece la posici�n de inicio para patrullar
        patrolStartPosition = transform.position;
    }

    private void Update()
    {
        // Verifica si el script FlashlightManager y el jugador existen
        if (flashlightManager == null || player == null || playerLife == null)
            return;

        // Si el retroceso est� activo, contin�a retrocediendo
        if (isKnockbackActive)
        {
            Knockback();
            return;
        }

        // Verifica si el jugador tiene la linterna encendida
        if (flashlightManager.isFlashlightOn && flashlightManager.flashlightEnergy > 0)
        {
            // Si la linterna est� encendida, el enemigo sigue al jugador
            isPatrolling = false; // Detiene la patrulla
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < detectionRange && !isWaitingAfterAttack)
            {
                MoveTowardsPlayer();
            }
        }
        else
        {
            // Si la linterna est� apagada, el enemigo patrulla
            isPatrolling = true; // Activa la patrulla
            Patrol();
        }
    }

    private void Patrol()
    {
        // Calcula la direcci�n de patrulla (de izquierda a derecha)
        float patrolDirection = Mathf.PingPong(Time.time * patrolSpeed, patrolWidth) - (patrolWidth / 2f);
        Vector3 patrolPosition = new Vector3(patrolStartPosition.x + patrolDirection, player.position.y + passiveHeight, transform.position.z);
        transform.position = Vector2.MoveTowards(transform.position, patrolPosition, speed * Time.deltaTime);

        // Actualiza la direcci�n del sprite del enemigo mientras patrulla
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
        // Obt�n la posici�n del jugador
        Vector3 playerPosition = player.position;

        // Aseg�rate de que el enemigo no descienda m�s all� de minHeight respecto al jugador
        if (transform.position.y < playerPosition.y + minHeight)
        {
            playerPosition.y = transform.position.y;  // Mant�n la altura del enemigo a un nivel constante si est� por debajo de minHeight
        }

        // Mueve el enemigo hacia el jugador, pero respetando la altura m�nima
        Vector2 direction = (playerPosition - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);

        // Actualiza la direcci�n del sprite del enemigo
        UpdateSpriteDirection(direction);
    }

    private void Knockback()
    {
        // Calcula la direcci�n de retroceso (en direcci�n opuesta al jugador)
        Vector2 knockbackDirection = (transform.position - player.position).normalized;

        // Mueve al enemigo hacia atr�s en la direcci�n actualizada del jugador
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)knockbackDirection, knockbackSpeed * Time.deltaTime);

        // Desactiva el retroceso despu�s de retroceder cierta distancia
        if (Vector2.Distance(transform.position, player.position) >= knockbackDistance)
        {
            isKnockbackActive = false;
            StartCoroutine("ResetAttack"); // Despu�s del retroceso, comienza el cooldown del ataque
        }

        // Actualiza la direcci�n del sprite del enemigo mientras retrocede
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
        isWaitingAfterAttack = true;  // Marca que est� esperando
        print("choco con jugador y espera cooldown");
        yield return new WaitForSeconds(attackCooldown); // Tiempo de espera entre ataques
        isWaitingAfterAttack = false; // El enemigo puede seguir al jugador de nuevo
        isKnockbackActive = false;
    }
}




