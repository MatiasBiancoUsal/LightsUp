using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class lifeCaracter : MonoBehaviour
{
    public static lifeCaracter instance;

    public int lives = 3;  // Número de vidas
    public Image[] batteryBars;
    public bool isDead = false;  // Nuevo booleano para indicar si está sin vidas

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void TakeDamage()
    {
        lives--;

        UpdateBatteryDisplay();

        if (lives <= 0)
        {
            GameOver();
            isDead = true;  // Marcar como muerto si no quedan vidas
        }
    }

    void UpdateBatteryDisplay()
    {
        for (int i = 0; i < batteryBars.Length; i++)
        {
            if (i < lives)
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
        TakeDamage();
        Respawn();
    }

    void Respawn()
    {
        LevelManager.instance.RespawnPlayer();
    }

    void GameOver()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }
}






