using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public int health = 3;
    public int maxHealth = 3;
    public Image[] batteryBars;

    public enum PlayerStates {
        Alive,
        Dead
    }

    public PlayerStates state;

    private void Awake()
    {
        if (instance == null)
        {
            state = PlayerStates.Alive;
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;

        UpdateBatteryDisplay();

        if (health <= 0)
        {
            state = PlayerStates.Dead;
            health = 0;
            LevelManager.instance.RespawnPlayer();
        }
    }

    void UpdateBatteryDisplay()
    {
        for (int i = 0; i < batteryBars.Length; i++)
        {
            if (i < health)
            {
                batteryBars[i].gameObject.SetActive(true);
            }
            else
            {
                batteryBars[i].gameObject.SetActive(false);
            }
        }
    }

    public void Fall()
    {
        ReceiveDamage(10);
    }

    public void resetPlayerHealth()
    {
        state = PlayerStates.Alive;
        health = maxHealth;
        FlashlightManager.instance.flashlightEnergy = FlashlightManager.instance.totalEnergy;
        UpdateBatteryDisplay();
        PlayerMov.instance.isRolling = false;
        PlayerMov.instance.isCrouched = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ReceiveDamage(1); 
        }
    }
}







