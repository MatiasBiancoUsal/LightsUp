using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.UI;

public class BrokenKey : MonoBehaviour
{
    public int keysCollected = 0;
    public int totalKeysRequired = 3;
    public DoorKey door;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] GameObject AnimBrokenKey;

    private bool hasPlayedVideo = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            keysCollected++;
            textMesh.text = keysCollected.ToString();
            Destroy(collision.gameObject);
        }

        if (keysCollected >= totalKeysRequired && !hasPlayedVideo)
        {
            hasPlayedVideo = true;
            Invoke("PlayVideo", 0f);
            Invoke("OpenDoor", 6f);

            if (textMesh != null)
            {
                Invoke("DestroyText", 1f);
            }
        }
    }

    void DestroyText()
    {
        Destroy(textMesh.gameObject);
    }

    void PlayVideo()
    {
        if (AnimBrokenKey != null)
        {
            Animator animator = AnimBrokenKey.GetComponent<Animator>();

            if (animator != null)
            {
                animator.SetTrigger("Play");
            }
        }
    }
    public void OpenDoor()
    {
        if (door != null)
        {
            DoorKey doorKey = door.GetComponent<DoorKey>();

            if (doorKey != null)
            {
                door.SetIsOpening(true);
            }
        }
    }
}

