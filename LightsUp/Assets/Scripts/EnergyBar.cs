using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnergyBar : MonoBehaviour
{
    public static EnergyBar instance;

    public Image energyBarImage;
    public GameObject player;

    private int collectedBatteries = 0; 
    public float batteryEnergy = 20f; 

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateEnergyBar();
    }

    private void Update()
    {
        if (FlashlightManager.instance.flashlightEnergy >= 0)
        {
            UpdateEnergyBar();
        }
        else
        {
            FlashlightManager.instance.FlashlightsOff();
        }

        if (Input.GetKeyDown(KeyCode.R) && collectedBatteries > 0)
        {
            UseBattery();
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
    }

    private void UseBattery()
    {
        FlashlightManager.instance.flashlightEnergy += batteryEnergy;

        if (FlashlightManager.instance.flashlightEnergy > FlashlightManager.instance.totalEnergy)
        {
            FlashlightManager.instance.flashlightEnergy = FlashlightManager.instance.totalEnergy;
        }

        collectedBatteries--;
        UpdateEnergyBar();
    }
}
