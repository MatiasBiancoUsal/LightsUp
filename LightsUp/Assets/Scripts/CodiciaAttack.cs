using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodiciaAttack : MonoBehaviour
{
    public static CodiciaAttack instance;

    private Animator animator;


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

    public void AttackPlayer()
    {
        CodiciaMov.instance.stopMovement();
        animator.SetBool("attack", true);
    }
}
