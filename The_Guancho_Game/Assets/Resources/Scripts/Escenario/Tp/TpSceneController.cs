using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TpSceneController : MonoBehaviour
{
    [SerializeField] private string escena;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            LoadSceneController.cargarEscena(escena);
        }
    }
}