using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    public GameObject[] flashlight;
    public int currentFlashlight = 0;
    public bool isFlashlight = false;
    public float flashlightEnergy = 100f;
    public float totalEnergy;
    public static FlashlightManager instance;
    // Start is called before the first frame update


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        totalEnergy = flashlightEnergy;    
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {
            if (isFlashlight)
            {
                flashlightEnergy -= 1f * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.F) && flashlightEnergy > 0)
            {
                if (isFlashlight)
                {
                    FlashlightsOff();
                    isFlashlight = false;
                }
                else
                {
                    flashlight[currentFlashlight].SetActive(true);
                    isFlashlight = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.G))
            {

                FlashlightsOff();
                if (currentFlashlight >= flashlight.Length - 1)
                {
                    currentFlashlight = 0;
                }
                else
                {
                    currentFlashlight++;
                }
                if (isFlashlight)
                {
                    flashlight[currentFlashlight].SetActive(true);
                }
            }
        }
    }

    public void addEnergy(float amount)
    {
        flashlightEnergy += amount;
        if (flashlightEnergy > totalEnergy)
        {
            flashlightEnergy = totalEnergy;
        }
    }


    public void FlashlightsOff()
    {
        for (int i = 0; i < flashlight.Length; i++)
        {
            flashlight[i].SetActive(false);
        }
    }
}
