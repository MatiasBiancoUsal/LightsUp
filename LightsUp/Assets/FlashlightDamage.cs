using UnityEngine;

public class FlashlightDamage : MonoBehaviour
{
    public float flashLightDistance = 5f; 
    public float flashlightAngle = 45f;
    public int numRays = 10; 
    private bool enemyDetected = false;

    void Update()
    {
        DetectObjectsInTriangle();
    }

    void DetectObjectsInTriangle()
    {
        Vector3 origin = transform.position;

        Vector3 direction = Vector2.right * transform.localScale.x;

        Vector3 leftVertex = origin + (Quaternion.Euler(0, 0, -flashlightAngle / 2) * direction) * flashLightDistance;
        Vector3 rightVertex = origin + (Quaternion.Euler(0, 0, flashlightAngle / 2) * direction) * flashLightDistance;

        enemyDetected = false;
        for (int i = 0; i <= numRays; i++)
        {
            float t = i / (float)numRays; 

            Vector3 rayEnd = Vector2.Lerp(leftVertex, rightVertex, t);

            RaycastHit2D hit = Physics2D.Raycast(origin, (rayEnd - origin).normalized, flashLightDistance);

            if (hit.collider != null)
            {
                enemyDetected = hit.collider.gameObject.tag == "Enemy";
            }

        }
    }

    void OnDrawGizmos()
    {
        Vector3 origin = transform.position;

        Vector3 direction = Vector2.right * transform.localScale.x;

        Vector3 leftVertex = origin + (Quaternion.Euler(0, 0, -flashlightAngle / 2) * direction) * flashLightDistance;
        Vector3 rightVertex = origin + (Quaternion.Euler(0, 0, flashlightAngle / 2) * direction) * flashLightDistance;

        // Dibujar el triángulo
        Gizmos.DrawLine(origin, leftVertex); 
        Gizmos.DrawLine(origin, rightVertex); 
        Gizmos.DrawLine(leftVertex, rightVertex); 

        // Dibujar los raycasts adicionales
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
