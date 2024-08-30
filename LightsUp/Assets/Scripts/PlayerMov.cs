using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    private float velocidad;
    public float velocidadCaminar;
    public float velocidadCorrer;
    private Rigidbody2D rb2d;
    public float Horizontal;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        velocidad = velocidadCaminar;
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidad = velocidadCorrer;
        } else
        {
            velocidad = velocidadCaminar;
        }

        rb2d.velocity = new Vector2(Horizontal * velocidad, rb2d.velocity.y);
    }

}
