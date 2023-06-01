using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SkyboxController : MonoBehaviour
{

    [SerializeField] private Material[] materialesSkybox;
    
    void Start()
    {
        RenderSettings.skybox = materialesSkybox[Random.Range(0, materialesSkybox.Length)];
    }
}
