using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraController : MonoBehaviour
{
    public float shakeDuration = 0;
    public float decreaseFactor = 2;

    private CinemachineVirtualCamera cinemachineVirtual;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    private void Awake()
    {
        cinemachineVirtual = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin =
            cinemachineVirtual.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        cameraShake();
    }

    public void cameraShake()
    {
        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
        }
        
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakeDuration;
    }
}
