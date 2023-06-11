using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaMuerteController : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void reintentar()
    {
        Cursor.lockState = CursorLockMode.Locked;
        LoadSceneController.cargarEscena(PlayerPrefs.GetString("EscenaAnterior"));
    }
}
