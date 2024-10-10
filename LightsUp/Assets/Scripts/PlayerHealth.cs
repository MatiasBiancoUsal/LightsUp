using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public Animator animator;

    public int health = 3;
    public int maxHealth = 3;
    public Image[] batteryBars;
    public float invincibleCounter;
    public float invincibleLength;

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

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if (invincibleCounter <= 0)
            {
                Physics2D.IgnoreLayerCollision(10, 2, false);
            }
        }
    }

    public void ReceiveDamage(int damage)
    {
        if (invincibleCounter <= 0)
        {
            health -= damage;

            animator.SetTrigger("damage");

            UpdateBatteryDisplay();

            Physics2D.IgnoreLayerCollision(10, 2, true);

            if (health <= 0)
            {
                state = PlayerStates.Dead;
                health = 0;
                //animatorta.instance.DeathAnimation();
                LevelManager.instance.RespawnPlayer();
            }

            invincibleCounter = invincibleLength;
        }
        else
        {

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


}







