using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera CinemachineVirtualCamera;
    public float ShakeIntensity = 3f;
    public float ShakeTime = 0.2f;

    private float timer;
    private CinemachineBasicMultiChannelPerlin _cbmcp;

    public static CameraShake instance;
    void Awake()
    {
        instance = this;
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        StopShake();
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = ShakeIntensity;

        timer = ShakeTime;
    }

    public void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;
        timer = 0;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                StopShake();
            }
        }
    }
}
