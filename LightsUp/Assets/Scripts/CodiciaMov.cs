using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodiciaMov : MonoBehaviour
{
    public Transform player; 
    public float speed = 3f;
    public float distance = 3f;
    private Animator animator;

    public static CodiciaMov instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (BossManager.instance.started)
        {
            moveToPlayer();
        }
    }

    private void moveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, new Vector2(player.position.x, transform.position.y)) < distance)
        {
            CodiciaAttack.instance.AttackPlayer();
        } else
        {
            animator.SetBool("attack", false);
        }
    }

    public void stopMovement()
    {
        speed = 0;
    }

    public void restartMovement()
    {
        speed = 3f;
    }
}
