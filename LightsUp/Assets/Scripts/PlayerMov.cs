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

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
        speed = speedWalk;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Caminar
        if (!PauseMenu.instance.isPaused)
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
        if (!isRolling)
        {
            //Correr
            if (Input.GetKey(KeyCode.LeftShift) && !isCrouched && !windZone)
            {
                isRunning = true;
                speed = speedRun;
            }
            else if (isCrouched)
            {
                speed = speedCrouch;
            } else
            {
                isRunning = false;
                speed = speedWalk;
            }

            rb2d.velocity = new Vector2(Horizontal * speed, rb2d.velocity.y);
        }

    }

    //Agachar
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

    //Rodar
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
