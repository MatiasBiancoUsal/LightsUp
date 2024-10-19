using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSAttack : MonoBehaviour
{
    public GameObject specialAttackPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L) && FlashlightManager.instance.flashlightEnergy >= 10 && !PauseMenu.instance.isPaused)
        {
            FlashlightManager.instance.spendEnergy(10);
            CameraShake.instance.ShakeCamera();
            Instantiate(specialAttackPrefab, PlayerMov.instance.transform.position, Quaternion.identity);
        }
    }
}
