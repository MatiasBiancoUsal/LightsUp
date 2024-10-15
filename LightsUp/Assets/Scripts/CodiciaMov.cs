using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodiciaMov : MonoBehaviour
{
    public Transform player; 
    public float speed = 3f;

    public static CodiciaMov instance;


    private void Awake()
    {
        instance = this;
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
        if (Mathf.Floor(transform.position.x) == Mathf.Floor(player.position.x))
        {
            CodiciaAttack.instance.AttackPlayer();
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
