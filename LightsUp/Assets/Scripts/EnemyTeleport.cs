using System.Collections;
using UnityEngine;

public class EnemyTeleport : MonoBehaviour
{
    public float teleportInterval = 2f;
    public Transform[] waypoints;

    private void Start()
    {
        StartCoroutine(TeleportRoutine());
    }

    private IEnumerator TeleportRoutine()
    {
        while (true)
        {
            TeleportToRandomWaypoint();
            yield return new WaitForSeconds(teleportInterval);
        }
    }

    private void TeleportToRandomWaypoint()
    {
        if (waypoints.Length == 0) return; 

        int randomIndex = Random.Range(0, waypoints.Length);
        Transform randomWaypoint = waypoints[randomIndex];

        transform.position = randomWaypoint.position;
    }
}

