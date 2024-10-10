using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushPull : MonoBehaviour
{
    public GameObject box;
    public bool isHoldingBox = false;
    private Collider2D currentBoxCollider;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHoldingBox && currentBoxCollider != null)
            {
                GrabBox();
            }
            else if (isHoldingBox)
            {
                ReleaseBox();
            }
        }

        if (isHoldingBox)
        {
            DragBox();
        }
    }

    void GrabBox()
    {
        box = currentBoxCollider.gameObject;
        isHoldingBox = true;
    }

    void ReleaseBox()
    {
        if (box != null)
        {
            isHoldingBox = false;
            box = null;
            currentBoxCollider = null;
        }
    }

    void DragBox()
    {
        if (box != null)
        {
            Rigidbody2D boxRigidbody = box.GetComponent<Rigidbody2D>();
            float offsetX = (box.transform.position.x < transform.position.x) ? -1.1f : 1.1f;
            Vector2 targetPosition = new Vector2(transform.position.x + offsetX, box.transform.position.y);

            
            boxRigidbody.MovePosition(targetPosition);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Box"))
        {
            currentBoxCollider = collision.collider;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Box"))
        {
            currentBoxCollider = null;
        }
    }
}





