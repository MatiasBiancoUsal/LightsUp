using UnityEngine;

public class GulaMov : MonoBehaviour
{
    public float speed = 2f;

    public GameObject pointA;
    public GameObject pointB;

    private Rigidbody2D Rigidbody;
    private Transform currentPoint;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

    void Update()
    {
        Rigidbody.velocity = new Vector2(currentPoint == pointB.transform ? speed : -speed, Rigidbody.velocity.y);

        if (transform.position.x >= currentPoint.position.x && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }
        else if (transform.position.x <= currentPoint.position.x && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
