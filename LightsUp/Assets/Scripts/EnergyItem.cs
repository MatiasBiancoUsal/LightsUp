using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyItem : MonoBehaviour
{
    public float energyAmount = 20f; 
    private EnergyBar energyBar;

    private void Start()
    {
       energyBar = Object.FindFirstObjectByType<EnergyBar>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.CompareTag("Player"))
        {
            if (energyBar != null)
            {
                energyBar.AddEnergy(energyAmount);
                Destroy(gameObject);
            }
        }
    }
}