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
    //[SerializeField] RawImage rawImage;       
    //[SerializeField] VideoPlayer videoPlayer;

    //private bool hasPlayedVideo = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Key"))
        {

           
            keysCollected++;
            textMesh.text = keysCollected.ToString();
    
            Destroy(collision.gameObject);

        }

        if (keysCollected >= totalKeysRequired /*&& !hasPlayedVideo*/)
        {
            //hasPlayedVideo = true;
            //Invoke("PlayVideo", 1f);

            if (door != null)
            {
                DoorKey doorKey = door.GetComponent<DoorKey>();

                if (doorKey != null)
                {
                    door.SetIsOpening(true);
                }
            }

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

    /*void PlayVideo()
    {
   
        rawImage.gameObject.SetActive(true);


        if (videoPlayer != null)
        {
            videoPlayer.Play();        
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {   
        rawImage.gameObject.SetActive(false);

        //GameObject door = GameObject.FindGameObjectWithTag("DoorMultipleKey");

        if (door != null)
        {
            DoorKey doorKey = door.GetComponent <DoorKey>();

            if (doorKey != null)
            {
               door.SetIsOpening(true);
            }
        }

        if (textMesh != null)
        {
            Destroy(textMesh.gameObject);
        }

    }*/

}
