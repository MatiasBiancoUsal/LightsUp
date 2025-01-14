using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Services.Analytics;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public Animator animator;

    public int health = 3;
    public int maxHealth = 3;
    public Image[] batteryBars;
    public float invincibleCounter;
    public float invincibleLength;

    public enum PlayerStates
    {
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
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if (invincibleCounter <= 0)
            {
                Physics2D.IgnoreLayerCollision(15, 2, false);
            }
        }
    }

    public void ReceiveDamage(int damage, string enemigo)
    {
        if (invincibleCounter <= 0)
        {
            health -= damage;

            animator.SetTrigger("damage");

            UpdateBatteryDisplay();

            Physics2D.IgnoreLayerCollision(15, 2, true);

            if (health <= 0)
            {
                state = PlayerStates.Dead;
                health = 0;

                // Activar la animación de muerte.
                animator.SetTrigger("death");

                // Para que la reaparición o finalización ocurra después de la animación de muerte,
                // puedes crear un método para ser llamado cuando la animación termine.
                Invoke("PlayerDeath", 1.5f); // Espera a que termine la animación (ajusta el tiempo si es necesario)

                if (SceneManager.GetActiveScene().name == "BossGula")
                {
                    CustomEvent eventoBoss = new CustomEvent("bossGula")
                {
                    {"VidaBoss",  BossHealth.Instance.health},
                    {"PorcentajeBateria", FlashlightManager.instance.flashlightEnergy * 100 / FlashlightManager.instance.totalEnergy},
                    {"NumBaterias", EnergyBar.instance.collectedBatteries },
                    {"UsoBaterias", EnergyBar.instance.usedBatteries },
                    {"coordenada_X", this.gameObject.transform.position.x},
                };
                    AnalyticsService.Instance.RecordEvent(eventoBoss);

                }

                if (SceneManager.GetActiveScene().name == "BossCodicia")
                {
                    CustomEvent eventoBoss = new CustomEvent("bossCodicia")
                {
                    {"VidaBoss",  BossHealth.Instance.health},
                    {"PorcentajeBateria", FlashlightManager.instance.flashlightEnergy * 100 / FlashlightManager.instance.totalEnergy},
                    {"NumBaterias", EnergyBar.instance.collectedBatteries },
                    {"UsoBaterias", EnergyBar.instance.usedBatteries },
                    {"coordenada_X", this.gameObject.transform.position.x},
                };
                    AnalyticsService.Instance.RecordEvent(eventoBoss);

                }
                else
                {
                    CustomEvent eventoMorir = new CustomEvent("morir")
                {
                   {"enemigo", enemigo},
                   {"level_index", SceneManager.GetActiveScene().buildIndex },
                   {"PorcentajeBateria", FlashlightManager.instance.flashlightEnergy * 100 / FlashlightManager.instance.totalEnergy},
                    {"NumBaterias", EnergyBar.instance.collectedBatteries },
                    {"UsoBaterias", EnergyBar.instance.usedBatteries },
                    {"coordenada_X", this.gameObject.transform.position.x},
                };
                    AnalyticsService.Instance.RecordEvent(eventoMorir);
                }






                //animatorta.instance.DeathAnimation();
                /*LevelManager.instance.RespawnPlayer();*/


            }

            invincibleCounter = invincibleLength;
        }

        else
        {

        }
    }
    void PlayerDeath()
    {
        LevelManager.instance.RespawnPlayer();
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
        ReceiveDamage(10, "Caida");
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















