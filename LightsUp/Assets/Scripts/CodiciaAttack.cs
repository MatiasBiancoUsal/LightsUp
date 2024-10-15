using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodiciaAttack : MonoBehaviour
{
    public static CodiciaAttack instance;

    private Animator animator;

    public GameObject attackCollider;
    public GameObject weakPoint;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void attackPlayer()
    {
        CodiciaMov.instance.stopMovement();
        animator.SetBool("attack", true);
    }

    public void enableCollider()
    {
        attackCollider.SetActive(true);
        enableWeakpoint();
    }

    public void enableWeakpoint() { 
        weakPoint.SetActive(true); 
    }

    public void disableCollider()
    {
        attackCollider.SetActive(false);
    } 

    public void disableWeakPoint()
    {
        weakPoint.SetActive(false);
    }
}
