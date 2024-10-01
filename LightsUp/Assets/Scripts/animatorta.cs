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

        
        animator.SetBool("isRunning", PlayerMov.instance.isRunning);

   //Animacion Idle

        if (PlayerMov.instance.Horizontal == 0 && PlayerMov.instance.GroundDetect())
        {
            animator.SetFloat("Y blend", 0);
            animator.SetFloat("X blend", 0);
        }
   //Animacion correr

        if (PlayerMov.instance.Horizontal != 0 && !PlayerMov.instance.isRunning)
        {
            animator.SetFloat("Y blend", 1);
            animator.SetFloat("X blend", -1);
        }

   //Animacion Jump 

        if (!PlayerMov.instance.GroundDetect())
        {
            animator.SetFloat("Y blend", 1);
            animator.SetFloat("X blend", 1);
        }
   
    }
}
