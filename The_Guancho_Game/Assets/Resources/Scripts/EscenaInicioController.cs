using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaInicioController : MonoBehaviour
{

    public void moverEscena(string escenaMover)
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(escenaMover);
    }

    public void cerrarJuego()
    {
        Application.Quit();
    }
}