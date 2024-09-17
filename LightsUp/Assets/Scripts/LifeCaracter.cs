using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeCaracter : MonoBehaviour
{
    public static LifeCaracter instance;

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

    // Método modificado para recibir un valor de daño
    public void ReceiveDamage(float damage)
    {
        // Asumimos que cada vida representa una cantidad fija de salud
        // Puedes ajustar esto según tu lógica de juego
        int damageAsLives = Mathf.CeilToInt(damage);

        lives -= damageAsLives;

        UpdateBatteryDisplay();

        if (lives <= 0)
        {
            lives = 0;  // Asegura que las vidas no sean negativas
            isDead = true;  // Marcar como muerto si no quedan vidas
            Invoke("GameOver", 1f);
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
        // Puedes ajustar el daño recibido aquí si es necesario
        ReceiveDamage(1);  // Asume que caer causa 1 unidad de daño
        Respawn();
    }

    void Respawn()
    {
        LevelManager.instance.RespawnPlayer();
    }

    void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Ajusta el valor de daño según el enemigo
            ReceiveDamage(1);  // Asume que el contacto con un enemigo causa 1 unidad de daño
        }
    }
}







