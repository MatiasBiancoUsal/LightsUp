using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class animatorta : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void Update()
    {

        // Set the running animation
        animator.SetBool("isRunning", PlayerMov.instance.isRunning);

        // Set the idle animation when the player is not moving
        if (PlayerMov.instance.Horizontal == 0 && PlayerMov.instance.GroundDetect())
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }

        // Transition to walking/running when the player is moving
        if (PlayerMov.instance.Horizontal != 0 && !PlayerMov.instance.isRunning)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Set the jumping animation
        if (!PlayerMov.instance.GroundDetect())
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
