using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancePlatform : MonoBehaviour
{
    public Rigidbody2D platformRigidbody;   
    public Transform playerTransform;        
    public float balanceForce = 10f;        
    public float maxRotation = 30f;         
    public float resetSpeed = 10f;          

    private bool playerOnPlatform = false;

    private void Start()
    {
        platformRigidbody.freezeRotation = false; 
    }

    void Update()
    {
        
        playerOnPlatform = IsPlayerOnPlatform();

        if (playerOnPlatform)
        {
            ApplyBalance(); 
        }
        else
        {
            
            CorrectPlatformRotation();
        }
    }

    
    bool IsPlayerOnPlatform()
    {
        float distance = Vector2.Distance(playerTransform.position, transform.position);
        float platformWidth = GetComponent<Collider2D>().bounds.size.x;

        return distance < platformWidth / 2f; 
    }

    
    void ApplyBalance()
    {
        float playerRelativePosition = playerTransform.position.x - transform.position.x;
        float appliedTorque = playerRelativePosition * balanceForce;

       
        if (platformRigidbody.rotation > maxRotation)
        {
            platformRigidbody.rotation = maxRotation;
        }
        else if (platformRigidbody.rotation < -maxRotation)
        {
            platformRigidbody.rotation = -maxRotation;
        }
        else
        {
           
            platformRigidbody.AddTorque(-appliedTorque);
        }
    }

   
    void CorrectPlatformRotation()
    {
       
        if (Mathf.Abs(platformRigidbody.rotation) > 0.1f) 
        {
            
            float newRotation = Mathf.MoveTowards(platformRigidbody.rotation, 0, resetSpeed * Time.deltaTime);
            platformRigidbody.rotation = newRotation; 
        }
    }
}











