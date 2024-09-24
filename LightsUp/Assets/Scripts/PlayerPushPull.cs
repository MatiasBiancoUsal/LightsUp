using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerPushPull : MonoBehaviour
{
    public GameObject box;
    private bool isHoldingBox = false;
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
        box.GetComponent<BoxCollider2D>().enabled = false;
        box.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    void ReleaseBox()
    {
        if (box != null)
        {
            isHoldingBox = false;
            box.GetComponent<BoxCollider2D>().enabled = true;
            box.GetComponent<Rigidbody2D>().isKinematic = false;
            box = null;
            currentBoxCollider = null;
        }
    }

    void DragBox()
    {
        if (box != null)
        {
            float offsetX = (box.transform.position.x < transform.position.x) ? -1.1f : 1.1f;
            box.transform.position = new Vector2(transform.position.x + offsetX, box.transform.position.y);
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

