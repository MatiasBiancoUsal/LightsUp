using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushPull : MonoBehaviour
{
    public float rayDistance = 5f;
    private GameObject pulledObject;
    private bool isPulling = false;
    public LayerMask Pushable;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, rayDistance, Pushable);

        if (Input.GetKeyDown(KeyCode.E) && hit.collider != null)
        {
            if (pulledObject == null)
            {
                pulledObject = hit.collider.gameObject;
                isPulling = true;
            }
            else if (pulledObject == hit.collider.gameObject)
            {
                StopDragging();
            }
        }

        else if (Input.GetKeyUp(KeyCode.E))
        {
            isPulling = false;
        }

        if (isPulling && pulledObject != null)
        {
            Drag();
        }
    }

    void Drag()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0);

        if (pulledObject != null)
        {
            Rigidbody2D rb = pulledObject.GetComponent<Rigidbody2D>();
            rb.velocity = moveDirection * 3f;
        }
    }

    void StopDragging()
    {
        if (pulledObject != null)
        {
            Rigidbody2D rb = pulledObject.GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            pulledObject = null;
            isPulling = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * rayDistance);
    }
}
