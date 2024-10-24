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
    private float Vertical;

    public Transform headCheck;
    public float headCheckLength;
    public LayerMask groundMask;

    private bool windZone;

    public bool stopInput;

    public bool isRunning;
    // public bool isWalking;
    private BoxCollider2D bc2d;

    [Header("Jump")]
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundCheckLength;

    [Header("Roll")]
    public bool isRolling = false;
    public bool canRoll = true;
    public float rollingTime;
    public float rollingCooldown;
    public float rollingPower;

    [Header("Crouch")]
    public float speedCrouch;
    public bool isCrouched = false;

    [Header("Climb")]
    public float speedClimb;
    private bool isLadder;
    private bool isClimbing;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed = speedWalk;
        animator = GetComponent<Animator>();
        bc2d = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (DialogueManager.Instance.isDialogueActive)
        {
            Horizontal = 0;
            Vertical = 0;
        }

        if (!PauseMenu.instance.isPaused && PlayerHealth.instance.state == PlayerHealth.PlayerStates.Alive && !stopInput && !DialogueManager.Instance.isDialogueActive)
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");

            if (!isRolling)
            {
                if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                Jump();
                Crouch();
                Climb();
            }
            Roll();
        }

        if (!isRolling && PlayerHealth.instance.state == PlayerHealth.PlayerStates.Alive)
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

        else
        {
            // Si el jugador está muerto, detener movimiento.
            if (!isRolling) 
            {
                rb2d.velocity = Vector2.zero;
            }

        }


    }

    private void FixedUpdate()
    {
        
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GroundDetect())
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            }
        }
    }

    public void Climb()
    {
        if (isLadder && Mathf.Abs(Vertical) > 0)
        {
            isClimbing = true;
        }

        if (isClimbing)
        {
            rb2d.gravityScale = 0f;
            rb2d.velocity = new Vector2(rb2d.velocity.x, Vertical * speedClimb);
        }
        else
        {
            rb2d.gravityScale = 1f;
        }
    }

    public void Crouch()
    {
        bool isHeadHitting = HeadDetect();

        if (Input.GetKeyDown(KeyCode.C) || isHeadHitting)
        {
            isCrouched = true;
            //animator.SetBool("Crouched", isCrouched);
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            if (!isHeadHitting)
            {
                isCrouched = false;
                //animator.SetBool("Crouched", isCrouched);
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

        if (groundCheck == null) return;

        Vector2 fromGround = groundCheck.position;
        Vector2 toGround = new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckLength);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(fromGround, toGround);
    }

    public bool GroundDetect()
    {
        bool hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckLength, groundMask);
        return hit;
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
            Physics2D.IgnoreLayerCollision(15, 2, true);
            canRoll = false;
            isRolling = true;
            rb2d.velocity = new Vector2(rb2d.velocity.x + rollingPower * transform.localScale.x, rb2d.velocity.y);
        
            yield return new WaitForSeconds(rollingTime);
            isRolling = false;

            yield return new WaitForSeconds(rollingCooldown);
            canRoll = true;
            Physics2D.IgnoreLayerCollision(15, 2, false);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wind"))
        {
            windZone = true;
        }

        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wind"))
        {
            windZone = false;
        }

        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}

