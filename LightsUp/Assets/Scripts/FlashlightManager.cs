using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    public GameObject[] Flashlight;
    public int currentFlashlight = 0;
    private bool isFlashlight = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
                if (isFlashlight)
                {
                    flashlightsOff();
                    isFlashlight = false;
                } else
                {
                    Flashlight[currentFlashlight].SetActive(true);
                    isFlashlight = true;
                }

        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            flashlightsOff();
            if (currentFlashlight >= Flashlight.Length - 1)
            {
                currentFlashlight = 0;
            } else
            {
                currentFlashlight++;
            }
            Flashlight[currentFlashlight].SetActive(true);

        }

    }

    void flashlightsOff()
    {
        for (int i = 0; i < Flashlight.Length; i++)
        {
            Flashlight[i].SetActive(false);
        }
    }
}
