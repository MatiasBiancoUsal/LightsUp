using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnergyBar : MonoBehaviour
{
    public Image energyBarImage; 
    public float maxEnergy = 100f; 
    public float currentEnergy;
    public float energyDecreaseRate = 1f;
    public GameObject player;

    private void Start()
    {
        currentEnergy = maxEnergy;
        UpdateEnergyBar();
    }

    private void Update()
    {
       
        if (currentEnergy > 0)
        {
            currentEnergy -= energyDecreaseRate * Time.deltaTime;
            UpdateEnergyBar();
        }
        else
        {
            HandlePlayerDeath();
        }
    }

    public void AddEnergy(float amount)
    {
        currentEnergy += amount;
        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        UpdateEnergyBar();
    }

    private void UpdateEnergyBar()
    {
        if (energyBarImage != null)
        {
            energyBarImage.fillAmount = currentEnergy / maxEnergy;
        }
    }

    private void HandlePlayerDeath()
    {
        if (player != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}