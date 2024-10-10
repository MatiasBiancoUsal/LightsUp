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

    void Start()
    {
        CameraManager.SwitchCamera(CameraStart);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            CameraManager.SwitchCamera(CameraFight);

            door.gameObject.GetComponent<Door>().SetIsOpening(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            CameraManager.SwitchCamera(CameraEnd);
        }
    }
}
