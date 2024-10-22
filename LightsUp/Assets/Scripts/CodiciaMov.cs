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

    public GameObject batteryPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("spawnBattery", 50f);
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
            CodiciaAttack.instance.attackPlayer();
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
        CodiciaAttack.instance.disableWeakPoint();
        speed = 3f;
    }

    private void spawnBattery()
    {
        Instantiate(batteryPrefab, new Vector3(transform.position.x, transform.position.y + -3f, transform.position.z), Quaternion.identity);
    }
}
