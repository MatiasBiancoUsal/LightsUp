using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed = 3f;
    public float detectionRange = 10f;
    public float attackRange = 1f;
    public float damage = 10f;
    public float minHeight = 1.5f;
    public float attackCooldown = 2f;
    public float passiveHeight = 5f;
    public float knockbackDistance = 3f; // Distancia de retroceso modificable desde el Inspector
    public float knockbackSpeed = 5f;  // Velocidad del retroceso
    public float patrolWidth = 5f;
    public float patrolSpeed = 2f;

    private Transform player;
    public bool isWaitingAfterAttack = false;
    private bool isKnockbackActive = false;
    private bool isPatrolling = false;

    private Vector3 knockbackTargetPosition; // Posición objetivo para el retroceso

    private FlashlightManager flashlightManager;
    private PlayerHealth playerHealth;
    private SpriteRenderer spriteRenderer;
    private Vector3 patrolStartPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        flashlightManager = FlashlightManager.instance;

        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        patrolStartPosition = transform.position;
    }

    private void Update()
    {
        if (flashlightManager == null || player == null)
            return;

        if (isKnockbackActive)
        {
            PerformKnockback();
            return;
        }

        if (flashlightManager.isFlashlightOn && flashlightManager.flashlightEnergy > 0)
        {
            isPatrolling = false;
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < detectionRange && !isWaitingAfterAttack)
            {
                MoveTowardsPlayer();
            }
        }
        else
        {
            isPatrolling = true;
            Patrol();
        }
    }

    private void Patrol()
    {
        float patrolDirection = Mathf.PingPong(Time.time * patrolSpeed, patrolWidth) - (patrolWidth / 2f);
        Vector3 patrolPosition = new Vector3(patrolStartPosition.x + patrolDirection, player.position.y + passiveHeight, transform.position.z);
        transform.position = Vector2.MoveTowards(transform.position, patrolPosition, speed * Time.deltaTime);

        UpdateSpriteDirection(Vector2.right * Mathf.Sign(patrolDirection));
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && flashlightManager.isFlashlightOn && flashlightManager.flashlightEnergy > 0)
        {
            // Activa el retroceso si el jugador tiene la linterna encendida
            isKnockbackActive = true;

            // Calcula la posición objetivo para el retroceso
            Vector2 knockbackDirection = (transform.position - player.position).normalized;
            knockbackTargetPosition = (Vector2)transform.position + knockbackDirection * knockbackDistance;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 playerPosition = player.position;

        if (transform.position.y < playerPosition.y + minHeight)
        {
            playerPosition.y = transform.position.y;
        }

        Vector2 direction = (playerPosition - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);

        UpdateSpriteDirection(direction);
    }

    private void PerformKnockback()
    {
        // Retrocede hacia la posición objetivo
        transform.position = Vector2.MoveTowards(transform.position, knockbackTargetPosition, knockbackSpeed * Time.deltaTime);

        // Verifica si se ha alcanzado la posición objetivo
        if (Vector2.Distance(transform.position, knockbackTargetPosition) < 0.1f)
        {
            isKnockbackActive = false; // Desactiva el retroceso
            StartCoroutine(ResetAttack());
        }

        // Actualiza la dirección del sprite
        Vector2 knockbackDirection = (knockbackTargetPosition - transform.position).normalized;
        UpdateSpriteDirection(knockbackDirection);
    }

    private void UpdateSpriteDirection(Vector2 direction)
    {
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private IEnumerator ResetAttack()
    {
        isWaitingAfterAttack = true;
        print("Chocó con el jugador y espera cooldown");
        yield return new WaitForSeconds(attackCooldown);
        isWaitingAfterAttack = false;
    }
}
