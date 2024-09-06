using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    public GameObject[] flashlight;
    public int currentFlashlight = 0;
    public bool isFlashlight = false;
    public static FlashlightManager instance;
    // Start is called before the first frame update


    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
                if (isFlashlight)
                {
                    FlashlightsOff();
                    isFlashlight = false;
                } else
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

    void FlashlightsOff()
    {
        for (int i = 0; i < flashlight.Length; i++)
        {
            flashlight[i].SetActive(false);
        }
    }
}
