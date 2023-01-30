using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtual;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    private void Awake()
    {
        cinemachineVirtual = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin =
            cinemachineVirtual.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void shakeCamera(float tiempoShake, float amountShake)
    {
        StartCoroutine(cameraShake(tiempoShake, amountShake));
    }

    IEnumerator cameraShake(float tiempoShake, float amountShake)
    {
        
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amountShake;
        
        yield return new WaitForSeconds(tiempoShake);
        
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
    }
}
