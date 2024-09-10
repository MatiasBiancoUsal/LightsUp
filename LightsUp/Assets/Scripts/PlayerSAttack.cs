using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSAttack : MonoBehaviour
{
    public GameObject specialAttackPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            Instantiate(specialAttackPrefab, PlayerMov.instance.transform.position, Quaternion.identity);
        }
    }
}
