using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public SpriteRenderer powerUpSpriteRenderer;
    public CircleCollider2D circleCollider;

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

        FlashlightDamage.Instance.flashLightDistance = 20f;

        FlashlightDamage.Instance.flashlightDamage = 1.5f;

        FlashlightDamage.Instance.flashlightAngle = 60f;

        yield return new WaitForSeconds(20f);

        FlashlightDamage.Instance.flashLightDistance = 10f;

        FlashlightDamage.Instance.flashlightDamage = .7f;

        FlashlightDamage.Instance.flashlightAngle = 40f;

        Destroy(gameObject);
    }
}
