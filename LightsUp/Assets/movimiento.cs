using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento : MonoBehaviour
{

    public float velocidad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Horizontal"))
        {
            transform.position += Vector3.right * velocidad * Time.deltaTime * Input.GetAxisRaw("Horizontal");
        }

        if (Input.GetButton("Vertical"))
        {
            transform.position += Vector3.up * velocidad * Time.deltaTime * Input.GetAxisRaw("Vertical");
        }
    }
}