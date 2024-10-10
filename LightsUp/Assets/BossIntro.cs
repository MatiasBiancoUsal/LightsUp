using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIntro : MonoBehaviour
{
    public GameObject door;
    public CinemachineVirtualCamera CameraStart;
    public CinemachineVirtualCamera CameraFight;
    public CinemachineVirtualCamera CameraEnd;

    public GameObject gulaHealthBar;

    void Start()
    {
        CameraManager.SwitchCamera(CameraStart);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CameraManager.SwitchCamera(CameraFight);

            door.gameObject.GetComponent<Door>().SetIsOpening(false);

            gulaHealthBar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CameraManager.SwitchCamera(CameraEnd);
        }
    }
}
