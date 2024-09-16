using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public static PlayerJump Instance;

    public float speed = 5f;        // Velocidad de movimiento del personaje
    public float jumpForce = 7f;    // Fuerza del salto
    private bool isGrounded;        // ¿Está el personaje en el suelo?
    private bool canDoubleJump;     // 

    private Rigidbody2D rb;         // Referencia al Rigidbody2D del personaje

    public Transform groundCheck;
    public float groundCheckLength;
    public LayerMask groundMask;

    private Animator animator;

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
        // Salto y doble salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GroundDetect())
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


    public bool GroundDetect()
    {
        bool hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckLength, groundMask);
        return hit;
    }

    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        Vector2 from = groundCheck.position;
        Vector2 to = new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckLength);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(from, to);
    }
}

