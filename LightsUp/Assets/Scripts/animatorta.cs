using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class animatorta : MonoBehaviour
{
    private Animator animator;
    private PlayerMov playerMovement;
    animatorta instancia;
    public float Horizontal;

    public void Awake()
    {
        instancia = this;

    }
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = PlayerMov.instance;

    }

    void Update()
    {

        animator.SetBool("isRunning", PlayerMov.instance.isRunning);
        Horizontal = Input.GetAxis("Horizontal");

        //Animacion Idle

        if (PlayerMov.instance.Horizontal == 0 && PlayerMov.instance.GroundDetect())
        {
            animator.SetFloat("Y blend", 0);
            animator.SetFloat("X blend", 0);
        }
   //Animacion caminar

        if (PlayerMov.instance.Horizontal != 0 && !PlayerMov.instance.isRunning)
        {
            animator.SetFloat("Y blend", 1);
            animator.SetFloat("X blend", -1);
        }

        else if (PlayerMov.instance.Horizontal == 0)
        {
            animator.SetFloat("Y blend", 0);
            animator.SetFloat("X blend", 0);
        }

        //Animacion correr

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat("Y blend", 1);
            animator.SetFloat("X blend", 0);
        }


    //Animacion Jump 

        if (Input.GetKey(KeyCode.Space) && !PlayerMov.instance.GroundDetect())
        {
            animator.SetFloat("Y blend", 1);
            animator.SetFloat("X blend", 1);
        }

     // Animamcion CrawlWalk
     if (Input.GetKeyDown(KeyCode.C) && PlayerMov.instance.Horizontal != 0)
        {
            animator.SetFloat("Y blend", 0.3f);
            animator.SetFloat("X blend", -1);
        }

     else if (Input.GetKeyUp(KeyCode.C) && PlayerMov.instance.Horizontal != 0)
        {
            animator.SetFloat("Y blend", 0);
            animator.SetFloat("X blend", 0);
        }

     if (Input.GetKey(KeyCode.X) && PlayerMov.instance.canRoll && PlayerMov.instance.Horizontal != 0)
        {
            animator.SetBool("isRolling", true);
        }
     else 
        {
            animator.SetBool("isRolling", false);
        }

    }
}
