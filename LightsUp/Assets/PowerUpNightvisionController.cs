using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PowerUpNightvisionController : MonoBehaviour
{
    public SpriteRenderer powerUpSpriteRenderer;
    public CircleCollider2D circleCollider;

    public GameObject globalLight;
    public GameObject nightvisionLight;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(PowerUpCo());
        }
    }

    IEnumerator PowerUpCo()
    {
        powerUpSpriteRenderer.enabled = false;

        circleCollider.enabled = false;

        PopupWindow.instance.AddToQueue("Vision nocturna");

        globalLight.SetActive(false);
        nightvisionLight.SetActive(true);

        yield return new WaitForSeconds(10f);

        nightvisionLight.SetActive(false);
        globalLight.SetActive(true);

        Destroy(gameObject);
    }
}
