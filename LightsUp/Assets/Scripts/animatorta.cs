using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class animatorta : MonoBehaviour
{
    private Animator animator;
    private PlayerMov playerMovement;
    private float Horizontal;
    public static animatorta instance;
    private PlayerPushPull playerPushPull;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = PlayerMov.instance;
        playerPushPull = GetComponent<PlayerPushPull>();
    }

    void Update()
    {
        // Animación Correr
        animator.SetBool("isRunning", playerMovement.isRunning);
        Horizontal = Input.GetAxis("Horizontal");

        // Animación Idle
        if (playerMovement.Horizontal == 0 && playerMovement.GroundDetect() && !playerMovement.isCrouched)
        {
            animator.SetFloat("Y blend", 0);
            animator.SetFloat("X blend", 0);
        }

        // Animación caminar
        else if (playerMovement.Horizontal != 0 && !playerMovement.isRunning)
        {
            animator.SetFloat("Y blend", 1);
            animator.SetFloat("X blend", -1);
        }

        // Animación correr
        if (Input.GetKey(KeyCode.LeftShift) && playerMovement.Horizontal != 0)
        {
            animator.SetFloat("Y blend", 1);
            animator.SetFloat("X blend", 0);
        }

        // Animación Jump 
        if (Input.GetKey(KeyCode.Space) && !playerMovement.GroundDetect())
        {
            animator.SetFloat("Y blend", 1);
            animator.SetFloat("X blend", 1);
        }

        // Animación Crouch
        animator.SetBool("isCrouched", playerMovement.Horizontal == 0 && playerMovement.isCrouched);

        // Animación CrawlWalk
        if (playerMovement.Horizontal != 0 && playerMovement.isCrouched)
        {
            animator.SetFloat("Y blend", 0.3f);
            animator.SetFloat("X blend", -1);
        }

        // Animación Roll
        animator.SetBool("isRolling", Input.GetKey(KeyCode.X) && playerMovement.canRoll && playerMovement.Horizontal != 0);

        // Animación PickUp
        animator.SetBool("isPickingUp", playerPushPull.isHoldingBox && playerMovement.Horizontal == 0);

        // Animación PickUpMove
        animator.SetBool("isPickingUpMoving", playerPushPull.isHoldingBox && playerMovement.Horizontal != 0);

        //Animacion Change

        if (Input.GetKey(KeyCode.F))
        {
            animator.SetFloat("Y blend", -1);
            animator.SetFloat("X blend", 0);
        }

    }

    // Animación Death
    public void DeathAnimation()
    {
        animator.SetTrigger("death");
    }
}
