using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform cameraTransform; 
    public float parallaxMultiplier; 

    private Vector3 previousCameraPosition;

    void Start()
    {
        previousCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        
        Vector3 deltaMovement = cameraTransform.position - previousCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier, 0);

        previousCameraPosition = cameraTransform.position;
    }
}
