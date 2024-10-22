using System.Collections.Generic;
using UnityEngine;

public class FlashlightDamage : MonoBehaviour
{
    public static FlashlightDamage Instance;

    public float flashLightDistance;
    public float flashlightAngle;
    public int numRays = 10;
    public float flashlightDamage;
    public GameObject flashlightObject;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        DetectEnemyFlashlight();
    }

    void DetectEnemyFlashlight()
    {
        Vector3 origin = flashlightObject.transform.position;

        Vector3 direction = Vector2.right * transform.localScale.x;

        Vector3 leftVertex = origin + (Quaternion.Euler(0, 0, -flashlightAngle / 2) * direction) * flashLightDistance;
        Vector3 rightVertex = origin + (Quaternion.Euler(0, 0, flashlightAngle / 2) * direction) * flashLightDistance;

        HashSet<Collider2D> hitEnemies = new HashSet<Collider2D>();
        HashSet<Collider2D> hitUV = new HashSet<Collider2D>();

        for (int i = 0; i <= numRays; i++)
        {
            float t = i / (float)numRays;

            Vector3 rayEnd = Vector2.Lerp(leftVertex, rightVertex, t);

            RaycastHit2D hit = Physics2D.Raycast(origin, (rayEnd - origin).normalized, flashLightDistance);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    if (hitEnemies.Add(hit.collider) && FlashlightManager.instance.isFlashlightOn && FlashlightManager.flashlightState == FlashlightManager.FlashlightState.FlashlightNormal)
                    {
                        if (hit.collider.gameObject.GetComponent<EnemyHealth>() != null)
                        {
                            hit.collider.gameObject.GetComponent<EnemyHealth>().ReceiveDamage(flashlightDamage);
                        }

                        if (hit.collider.gameObject.GetComponent<BossManager>() != null)
                        {
                            hit.collider.gameObject.GetComponent<BossManager>().ReceiveDamage(flashlightDamage);
                        }
                    }
                }



                if (hit.collider.CompareTag("UV"))
                {
                    if (hitUV.Add(hit.collider) && FlashlightManager.instance.isFlashlightOn && FlashlightManager.flashlightState == FlashlightManager.FlashlightState.FlashlightUV)
                    {
                        hit.collider.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 255);
                        hit.collider.GetComponent<BoxCollider2D>().isTrigger = false;
                    }
                }
            }

        }
    }

    void OnDrawGizmos()
    {
        Vector3 origin = flashlightObject.transform.position;

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

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Gizmos.color = Color.green;
                }
                else if (hit.collider.CompareTag("UV"))
                {
                    Gizmos.color = Color.cyan;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawLine(origin, rayEnd);
        }

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(origin, flashLightDistance);
    }
}