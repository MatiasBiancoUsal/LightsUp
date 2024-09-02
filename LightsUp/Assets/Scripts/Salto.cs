using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salto : MonoBehaviour
{
    public static Salto Instance;

    public float speed = 5f;        // Velocidad de movimiento del personaje
    public float jumpForce = 7f;    // Fuerza del salto
    private bool isGrounded;        // ¿Está el personaje en el suelo?
    private bool canDoubleJump;     // 

    private Rigidbody2D rb;         // Referencia al Rigidbody2D del personaje

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento horizontal
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Salto y doble salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = false;  // Habilita el doble salto
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = false; // Desactiva el doble salto después de usarlo
            }
        }
    }

    // Verifica si el personaje está tocando el suelo o una plataforma
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
}

