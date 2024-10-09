using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
   public Transform ShotController;

   public float distanceLine;

   public LayerMask PlayerLayer;

   public bool PlayerRange;

   public GameObject EnemyBullet;

    public float timeBetweenShots;
        
    public float timeLastShot;

    public float timeWaitShot;
   private void Update()
    {
        PlayerRange = Physics2D.Raycast(ShotController.position, transform.right, distanceLine, PlayerLayer);

        if (PlayerRange)
        {
            if (Time.time > timeBetweenShots + timeLastShot)
            {
                timeLastShot = Time.time;
                Shot();
            }
        }

    }
    private void Shot()
    {
        EnemyBullet bullet = Instantiate(EnemyBullet, ShotController.position, ShotController.rotation).GetComponent<EnemyBullet>();

        Vector2 direction = (FindObjectOfType<PlayerMov>().transform.position - ShotController.position).normalized;

        bullet.SetDirection(direction);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(ShotController.position, ShotController.position + transform.right * distanceLine);
    }
   
}
