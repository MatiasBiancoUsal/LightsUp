using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class EnergyBar : MonoBehaviour
{
    public static EnergyBar instance;

    public Image energyBarImage;
    public GameObject player;
    public Text batteryCount; 

    public int collectedBatteries = 0;
    public int usedBatteries = 0;
    public float batteryEnergy = 20f; 

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        usedBatteries = 0;
        UpdateEnergyBar();
        UpdateBatteryCount();
    }

    private void Update()
    {
        if (PlayerHealth.instance.state == PlayerHealth.PlayerStates.Dead)
        {
            usedBatteries = 0;
        }

        if (FlashlightManager.instance.flashlightEnergy > 0)
        {
            UpdateEnergyBar();
        }
        else
        {
            FlashlightManager.instance.FlashlightOff();
        }

        if (Input.GetKeyDown(KeyCode.R) && collectedBatteries > 0)
        {
            UseBattery();
            usedBatteries++;
        }
    }

    private void UpdateEnergyBar()
    {
        if (energyBarImage != null)
        {
            energyBarImage.fillAmount = FlashlightManager.instance.flashlightEnergy / FlashlightManager.instance.totalEnergy;
        }
    }

    public void CollectBattery()
    {
        collectedBatteries++;
        UpdateBatteryCount(); 
    }

    private void UseBattery()
    {
        FlashlightManager.instance.flashlightEnergy += batteryEnergy;

        if (FlashlightManager.instance.flashlightEnergy > FlashlightManager.instance.totalEnergy)
        {
            FlashlightManager.instance.flashlightEnergy = FlashlightManager.instance.totalEnergy;
        }

        collectedBatteries--; 
        UpdateBatteryCount(); 
        UpdateEnergyBar();
    }

    private void UpdateBatteryCount()
    {
        batteryCount.text = " " + collectedBatteries.ToString();
    }
}