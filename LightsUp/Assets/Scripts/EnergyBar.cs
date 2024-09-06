using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnergyBar : MonoBehaviour
{
    public static EnergyBar instance;

    public Image energyBarImage; 
    public float maxEnergy = 100f; 
    public float currentEnergy;
    public float energyDecreaseRate = 1f;
    public GameObject player;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentEnergy = maxEnergy;
        UpdateEnergyBar();
    }

    private void Update()
    {
       
        if (currentEnergy > 0 && FlashlightManager.instance.isFlashlight)
        {
            currentEnergy -= energyDecreaseRate * Time.deltaTime;
            UpdateEnergyBar();
        }
        else
        {
            FlashlightManager.instance.FlashlightsOff();
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

}