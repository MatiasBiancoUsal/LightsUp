using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public int life = 100;
    public GameObject PointA;
    public GameObject PointB;
    public float speed;
    private Rigidbody2D Rigidbody;
    //private Animator anim; -- Comentado para cuando exista un sprite con animación. -- 
    private Transform currentPoint;


    void Start()
    { 
        Rigidbody = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        currentPoint = PointB.transform;
        //anim.SetBool("isRunning", true);
    }
    public void RecieveDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        Vector2 Point = currentPoint.position - transform.position;
        if (currentPoint == PointB.transform)
        {
            Rigidbody.velocity = new Vector2(speed, Rigidbody.velocity.y);
        }
        else 
        {
            Rigidbody.velocity = new Vector2(-speed, Rigidbody.velocity.y);
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointB.transform) 
        {
            flip();
            currentPoint = PointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointA.transform)
        {
            flip();
            currentPoint = PointB.transform;
        }
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
    }
}
