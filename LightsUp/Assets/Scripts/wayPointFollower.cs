using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int CurrentwaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    private void Update()
    {
        if (Vector2.Distance(waypoints[CurrentwaypointIndex].transform.position, transform.position) < 0.1f)
        {
            CurrentwaypointIndex++;
            if (CurrentwaypointIndex >= waypoints.Length)
            {
                CurrentwaypointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[CurrentwaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
