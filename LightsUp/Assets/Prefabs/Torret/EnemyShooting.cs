using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 1.0f;
    public float nextFire = 0.0f;
    public float timeToDisappear = 5f;

    public Transform player; // Reference to the player
    public float shootRange = 10f; // Range to start shooting
    private bool isInRange = false;

    void Update()
    {
        // Check if the player is within range
        if (Vector3.Distance(transform.position, player.position) <= shootRange)
        {
            if (!isInRange)
            {
                isInRange = true; // Player is now in range
            }

            // Handle shooting
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }
        }
        else
        {
            if (isInRange)
            {
                isInRange = false; // Player is out of range
            }
        }
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Torret2 bulletScript = bullet.GetComponent<Torret2>();
        if (bulletScript != null)
        {
            bulletScript.target = player; // Set the player as the target for the bullet
        }
    }

}