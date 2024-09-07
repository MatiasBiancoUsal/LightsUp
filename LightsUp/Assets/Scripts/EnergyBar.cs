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
       
        if (FlashlightManager.instance.flashlightEnergy > 0 && FlashlightManager.instance.isFlashlight)
        {
            UpdateEnergyBar();
        }
        else
        {
            FlashlightManager.instance.FlashlightsOff();
        }
    }


    private void UpdateEnergyBar()
    {
        if (energyBarImage != null)
        {
            energyBarImage.fillAmount = FlashlightManager.instance.flashlightEnergy / FlashlightManager.instance.totalEnergy;
        }
    }

}