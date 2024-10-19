using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightManager : MonoBehaviour
{
    public GameObject FlashlightNormal;
    public GameObject FlashlightUV;
    // public GameObject FlashlightIR;

    public float flashlightEnergy = 100f;
    public float totalEnergy;

    public static FlashlightManager instance;

    public enum FlashlightState
    {
        FlashlightNormal,
        FlashlightUV,
        // FlashlightIR
    }

    public static FlashlightState flashlightState;
    public bool isFlashlightOn;

    public string[] enemyTags = { "Enemy" };

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        totalEnergy = flashlightEnergy;
        flashlightState = FlashlightState.FlashlightNormal;
        isFlashlightOn = false;
        setFlashlightState(flashlightState);
    }

    void Update()
    {
        if (DialogueManager.Instance.isDialogueActive) 
        {
            FlashlightOff();
        }

        if (!PauseMenu.instance.isPaused && !DialogueManager.Instance.isDialogueActive)
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
                FlashlightOff();
            }

            //if (flashlightState == FlashlightState.FlashlightIR)
            //{
            //    GameObject scenery = GameObject.Find("Scenery");

            //    if (scenery != null)
            //    {
            //        Renderer[] allRenderers = scenery.GetComponentsInChildren<Renderer>();
            //        Transform[] allTransforms = scenery.GetComponentsInChildren<Transform>();

            //        foreach (Renderer rend in allRenderers)
            //        {
            //            foreach (Material mat in rend.materials)
            //            {
            //                if (mat.HasProperty("_Color"))
            //                {
            //                    Color color = mat.color;
            //                    color.a = Mathf.Clamp01(0.5f);
            //                    mat.color = color;
            //                }
            //            }
            //        }

            //        foreach (Transform transform in allTransforms)
            //        {
            //            GameObject gameObject = transform.gameObject;
            //            if (gameObject.GetComponent<ShadowCaster2D>()) gameObject.GetComponent<ShadowCaster2D>().enabled = false;
            //        }
            //    }

            //    foreach (string enemyTag in enemyTags)
            //    {
            //        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

            //        foreach (GameObject enemy in enemies)
            //        {
            //            enemy.transform.GetChild(0).gameObject.SetActive(true);
            //        }
            //    }
            //}
            //else
            //{
            //    ExitIR();
            //}
        }
        
    }

    private void ExitIR()
    {
        //GameObject scenery = GameObject.Find("Scenery");

        //if (scenery != null)
        //{
        //    Renderer[] allRenderers = scenery.GetComponentsInChildren<Renderer>();
        //    Transform[] allTransforms = scenery.GetComponentsInChildren<Transform>();

        //    foreach (Renderer rend in allRenderers)
        //    {
        //        foreach (Material mat in rend.materials)
        //        {
        //            if (mat.HasProperty("_Color"))
        //            {
        //                Color color = mat.color;
        //                color.a = Mathf.Clamp01(1f);
        //                mat.color = color;
        //            }
        //        }
        //    }

        //    foreach (Transform transform in allTransforms)
        //    {
        //        GameObject gameObject = transform.gameObject;
        //        if (gameObject.GetComponent<ShadowCaster2D>()) gameObject.GetComponent<ShadowCaster2D>().enabled = true;
        //    }
        //}

        //foreach (string enemyTag in enemyTags)
        //{
        //    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        //    foreach (GameObject enemy in enemies)
        //    {
        //        if (enemy.transform.GetChild(0).gameObject != null)
        //        {
        //        enemy.transform.GetChild(0).gameObject.SetActive(false);

        //        }
        //    }
        //}
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
                flashlightState = FlashlightState.FlashlightNormal;
                break;
            //case FlashlightState.FlashlightIR:
            //    flashlightState = FlashlightState.FlashlightNormal;
            //    break;
        }

        setFlashlightState(flashlightState);
    }

    private void setFlashlightState(FlashlightState state)
    {
        FlashlightOff();

        switch (state)
        {
            case FlashlightState.FlashlightNormal:
                FlashlightNormal.SetActive(isFlashlightOn);
                break;
            case FlashlightState.FlashlightUV:
                FlashlightUV.SetActive(isFlashlightOn);
                break;
            //case FlashlightState.FlashlightIR:
            //    FlashlightIR.SetActive(isFlashlightOn);
            //    break;
        }
    }

    public void FlashlightOff()
    {
        FlashlightNormal.SetActive(false);
        FlashlightUV.SetActive(false);
        // FlashlightIR.SetActive(false);
        // ExitIR();
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
