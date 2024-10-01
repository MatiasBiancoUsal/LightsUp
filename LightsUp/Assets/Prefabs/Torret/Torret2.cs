using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torret2 : MonoBehaviour
{
    public float speed = 10.0f; // Velocidad de la bala
    public Transform target; // El jugador
    private Vector3 direction;

    

    void Start()
    {
        // Calcula la dirección hacia el objetivo y normaliza
        direction = (target.position - transform.position).normalized;
       
    }

    void Update()
    {
        // Mueve la bala en la dirección calculada
        transform.position += direction * speed * Time.deltaTime;

       
    }

}
