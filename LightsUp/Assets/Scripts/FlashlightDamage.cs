using System.Collections.Generic;
using UnityEngine;

public class FlashlightDamage : MonoBehaviour
{
    public float flashLightDistance = 5f; 
    public float flashlightAngle = 45f;
    public int numRays = 10;
    public float flashlightDamage;

    void Update()
    {
        DetectEnemyFlashlight();
    }

    void DetectEnemyFlashlight()
    {
        Vector3 origin = transform.position;

        Vector3 direction = Vector2.right * transform.localScale.x;

        Vector3 leftVertex = origin + (Quaternion.Euler(0, 0, -flashlightAngle / 2) * direction) * flashLightDistance;
        Vector3 rightVertex = origin + (Quaternion.Euler(0, 0, flashlightAngle / 2) * direction) * flashLightDistance;

        HashSet<Collider2D> hitEnemies = new HashSet<Collider2D>();

        for (int i = 0; i <= numRays; i++)
        {
            float t = i / (float)numRays; 

            Vector3 rayEnd = Vector2.Lerp(leftVertex, rightVertex, t);

            RaycastHit2D hit = Physics2D.Raycast(origin, (rayEnd - origin).normalized, flashLightDistance);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    if (hitEnemies.Add(hit.collider) && FlashlightManager.instance.isFlashlight)
                    {
                        hit.collider.gameObject.GetComponent<EnemyHealth>().ReceiveDamage(flashlightDamage);
                    }
                }
            }

        }
    }

    void OnDrawGizmos()
    {
        Vector3 origin = transform.position;

        Vector3 direction = Vector2.right * transform.localScale.x;

        Vector3 leftVertex = origin + (Quaternion.Euler(0, 0, -flashlightAngle / 2) * direction) * flashLightDistance;
        Vector3 rightVertex = origin + (Quaternion.Euler(0, 0, flashlightAngle / 2) * direction) * flashLightDistance;

        Gizmos.DrawLine(origin, leftVertex); 
        Gizmos.DrawLine(origin, rightVertex); 
        Gizmos.DrawLine(leftVertex, rightVertex); 

        for (int i = 0; i <= numRays; i++)
        {
            float t = i / (float)numRays;
            Vector3 rayEnd = Vector2.Lerp(leftVertex, rightVertex, t);
            RaycastHit2D hit = Physics2D.Raycast(origin, (rayEnd - origin).normalized, flashLightDistance);

            Gizmos.color = hit.collider != null && hit.collider.CompareTag("Enemy") ? Color.green : Color.red;
            Gizmos.DrawLine(origin, rayEnd);
        }

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(origin, flashLightDistance);
    }
}
