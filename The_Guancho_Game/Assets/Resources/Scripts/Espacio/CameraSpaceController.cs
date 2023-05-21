using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSpaceController : MonoBehaviour
{
    private CinemachineVirtualCamera cm;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    [SerializeField] private float amountShakeMin = 5;
    [SerializeField] private float amountShakeMax = 10;

    private void Awake()
    {
        cm = GetComponentInChildren<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin =
            cm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void setMinShake()
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amountShakeMin;
    }

    public void setMaxShake()
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amountShakeMax;
    }
}
