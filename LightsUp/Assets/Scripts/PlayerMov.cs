using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public static PlayerMov Instance;

    private float speed;
    public float speedWalk;
    public float speedRun;
    private Rigidbody2D rb2d;
    private Animator animator;
    public float Horizontal;

    public bool isRunning, isCrouched;

    private void Awake()
    {
        Instance = this;
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
        Horizontal = Input.GetAxis("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Crouch();
    }

    private void FixedUpdate()
    {
        //Correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = speedRun;
        } else
        {
            speed = speedWalk;
        }

        rb2d.velocity = new Vector2(Horizontal * speed, rb2d.velocity.y);
    }

    //Crouch
    public void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isCrouched = true;
            Debug.Log("Anda");
            animator.SetBool("Crouched", isCrouched);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            isCrouched = false;
            animator.SetBool("Crouched", isCrouched);
        }
    }
}
