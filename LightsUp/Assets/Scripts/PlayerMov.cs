using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public static PlayerMov instance;

    private float speed;
    public float speedWalk;
    public float speedRun;
    private Rigidbody2D rb2d;
    private Animator animator;
    public float Horizontal;

    public bool isRunning;
   // public bool isWalking;


    [Header("Roll")]
    public bool isRolling = false;
    private bool canRoll = true;
    public float rollingTime;
    public float rollingCooldown;
    public float rollingPower;

    [Header("Crouch")]
    public float speedCrouch;
    public bool isCrouched = false;

    public Transform headCheck;
    public float headCheckLength;
    public LayerMask groundMask;

    private bool windZone;

    // Booleano para saber si el personaje está sin vidas
    public bool isDead = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed = speedWalk;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Actualizar el valor de isDead desde lifeCaracter
        isDead = lifeCaracter.instance.isDead;

        if (!PauseMenu.instance.isPaused && !isDead)  // Asegurarse de que el jugador no se mueva si está muerto
        {
            Horizontal = Input.GetAxis("Horizontal");

            if (!isRolling)
            {
                if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                Crouch();
            }

            Roll();
        }
    }

    private void FixedUpdate()
    {
        if (!isRolling && !isDead)  // Evitar que se mueva si está muerto
        {
            if (Input.GetKey(KeyCode.LeftShift) && !isCrouched && !windZone)
            {
                isRunning = true;
                speed = speedRun;
              //  isWalking = false;
                // animator.SetBool("isRunning", true);
              //  animator.SetBool("isRunning", !isWalking);
            }
            else if (isCrouched)
            {
                speed = speedCrouch;
            }
            else
            {
                isRunning = false;
                speed = speedWalk;
               // animator.SetBool("isWalking", !isRunning);
            }

            rb2d.velocity = new Vector2(Horizontal * speed, rb2d.velocity.y);
        }
    }

    public void Crouch()
    {
        bool isHeadHitting = HeadDetect();

        if (Input.GetKeyDown(KeyCode.C) || isHeadHitting)
        {
            isCrouched = true;
            animator.SetBool("Crouched", isCrouched);
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            if (!isHeadHitting)
            {
                isCrouched = false;
                animator.SetBool("Crouched", isCrouched);
            }
        }
    }

    public bool HeadDetect()
    {
        bool hit = Physics2D.Raycast(headCheck.position, Vector2.up, headCheckLength, groundMask);
        return hit;
    }

    private void OnDrawGizmos()
    {
        if (headCheck == null) return;

        Vector2 from = headCheck.position;
        Vector2 to = new Vector2(headCheck.position.x, headCheck.position.y + headCheckLength);

        Gizmos.DrawLine(from, to);
    }

    public void Roll()
    {
        if (Input.GetKeyDown(KeyCode.X) && canRoll)
        {
            StartCoroutine(StartRoll());
        }
    }

    IEnumerator StartRoll()
    {
        if (Horizontal != 0 && !isCrouched)
        {
            canRoll = false;
            isRolling = true;
            animator.SetBool("Roll", isRolling);
            rb2d.velocity = new Vector2(rb2d.velocity.x + (rollingPower * transform.localScale.x), rb2d.velocity.y);
            yield return new WaitForSeconds(rollingTime);
            isRolling = false;
            animator.SetBool("Roll", isRolling);
            yield return new WaitForSeconds(rollingCooldown);
            canRoll = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wind"))
        {
            windZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wind"))
        {
            windZone = false;
        }
    }
}

