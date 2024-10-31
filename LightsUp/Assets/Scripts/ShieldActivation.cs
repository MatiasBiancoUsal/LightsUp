using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldActivation : MonoBehaviour
{
    public GameObject shieldPrefab; 
    private GameObject activeShield;
    private int protection;

   
    public Vector3 shieldOffset = new Vector3(0, 0.5f, 0); 

    void Update()
    {
        
        if (activeShield != null)
        {
            activeShield.transform.position = transform.position + shieldOffset; 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Protection"))
        {
            protection = 2; 
            Destroy(other.gameObject); 
            ActivateShield();
        }

        if (other.CompareTag("Enemy"))
        {
           
            if (protection > 0)
            {
                protection--; 


                
                if (protection <= 0 && activeShield != null)
                {
                    Destroy(activeShield);
                    activeShield = null; 
                }
            }
            else
            {
                
            }
        }
    }

    void ActivateShield()
    {
       
        if (activeShield == null) 
        {
            activeShield = Instantiate(shieldPrefab, transform.position + shieldOffset, Quaternion.identity);
            activeShield.transform.SetParent(transform); 
        }
    }
}
