using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class animatorta : MonoBehaviour
{
    private Animator animator;
    private PlayerMov playerMov; // Reference to the PlayerMov script
    private PlayerJump playerJump; // Reference to the PlayerJump script

    void Start()
    {
        // Get references to the animator, PlayerMov, and PlayerJump components
        animator = GetComponent<Animator>();
        playerMov = PlayerMov.instance;
        playerJump = PlayerJump.Instance;
    }

    void Update()
    {
        // Update animator parameters based on the player movement script

        // Set the running animation
        animator.SetBool("isRunning", playerMov.isRunning);

        // Set the idle animation when the player is not moving
        if (playerMov.Horizontal == 0 && playerJump.GroundDetect())
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }

        // Transition to walking/running when the player is moving
        if (playerMov.Horizontal != 0 && !playerMov.isRunning)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Set the jumping animation
        if (!playerJump.GroundDetect())
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

      //  if (Input.GetKeyDown(KeyCode.C))
      //  {
        //    animator.SetBool("isCrawling", true);
        //}

        //else if(Input.GetKeyDown(KeyCode.C))
        //{
          //  animator.SetBool("isCrawling", false);
        //}
    }
}
