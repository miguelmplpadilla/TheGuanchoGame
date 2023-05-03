using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicadorGanchoController : MonoBehaviour
{
    private GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameObject.Find("MainCamera");
    }

    private void FixedUpdate()
    {
        transform.LookAt(mainCamera.transform);
    }
}
