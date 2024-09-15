using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    public GameObject FlashlightNormal;
    public GameObject FlashlightUV;
    public GameObject FlashlightIR;

    public float flashlightEnergy = 100f;
    public float totalEnergy;

    public static FlashlightManager instance;

    public enum FlashlightState
    {
        FlashlightNormal,
        FlashlightUV,
        FlashlightIR
    }

    public static FlashlightState flashlightState;
    public bool isFlashlightOn;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        totalEnergy = flashlightEnergy;
        flashlightState = FlashlightState.FlashlightNormal; // Default to the first flashlight
        isFlashlightOn = false;
        setFlashlightState(flashlightState); // Ensure initial flashlight state
    }

    void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {
            handleFlashlightState();
            handleInput();
        }
    }

    private void handleFlashlightState()
    {
        if (isFlashlightOn)
        {
            flashlightEnergy -= 1f * Time.deltaTime;
            if (flashlightEnergy <= 0)
            {
                flashlightEnergy = 0;
                isFlashlightOn = false;
                FlashlightOff(); // Ensure the flashlight is turned off
            }
        }
    }

    private void handleInput()
    {
        if (Input.GetKeyDown(KeyCode.F) && flashlightEnergy > 0)
        {
            toggleFlashlight();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            switchFlashlight();
        }
    }

    public void toggleFlashlight()
    {
        if (isFlashlightOn)
        {
            FlashlightOff();
            isFlashlightOn = false;
        }
        else
        {
            isFlashlightOn = true;
            setFlashlightState(flashlightState);
        }
    }

    public void switchFlashlight()
    {

        switch (flashlightState)
        {
            case FlashlightState.FlashlightNormal:
                flashlightState = FlashlightState.FlashlightUV;
                break;
            case FlashlightState.FlashlightUV:
                flashlightState = FlashlightState.FlashlightIR;
                break;
            case FlashlightState.FlashlightIR:
                flashlightState = FlashlightState.FlashlightNormal;
                break;
        }

        setFlashlightState(flashlightState);
    }

    private void setFlashlightState(FlashlightState state)
    {
        FlashlightOff(); // Ensure all flashlights are off before setting the new state

        switch (state)
        {
            case FlashlightState.FlashlightNormal:
                FlashlightNormal.SetActive(isFlashlightOn);
                break;
            case FlashlightState.FlashlightUV:
                FlashlightUV.SetActive(isFlashlightOn);
                break;
            case FlashlightState.FlashlightIR:
                FlashlightIR.SetActive(isFlashlightOn);
                break;
        }
    }

    public void FlashlightOff()
    {
        FlashlightNormal.SetActive(false);
        FlashlightUV.SetActive(false);
        FlashlightIR.SetActive(false);
    }

    public void addEnergy(float amount)
    {
        flashlightEnergy += amount;
        if (flashlightEnergy > totalEnergy)
        {
            flashlightEnergy = totalEnergy;
        }
    }

    public void spendEnergy(float amount)
    {
        flashlightEnergy -= amount;
        if (flashlightEnergy < 0)
        {
            flashlightEnergy = 0;
        }
    }
}
