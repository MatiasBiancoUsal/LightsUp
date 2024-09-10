using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite checkpointOn, checkpointOff;

    public GameObject checkpointLight;

    public CircleCollider2D circleCollider;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(CheckpointCo());
        }
    }

    public void ResetCheckpoint()
    {
        spriteRenderer.sprite = checkpointOff;
        checkpointLight.gameObject.SetActive(false);
    }

    IEnumerator CheckpointCo()
    {
        CheckpointController.instance.DeactivateCheckpoints();
        spriteRenderer.sprite = checkpointOn;
        checkpointLight.gameObject.SetActive(true);

        CheckpointController.instance.SetSpawnPoint(transform.position);

        yield return new WaitForSeconds(1f);

        circleCollider.enabled = false;

    }
}
