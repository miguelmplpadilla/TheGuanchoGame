using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaController : MonoBehaviour
{

    public bool pausado = false;

    [SerializeField] private GameObject canvasPausa;

    private void Update()
    {
        if (Input.GetButtonDown("Pausa"))
        {
            pausarDespausar();
        }
    }

    public void pausarDespausar()
    {

        if (Time.timeScale == 1)
        {
            canvasPausa.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            canvasPausa.SetActive(false);
            Time.timeScale = 1;
        }
        
        pausado = !pausado;
    }

    public void volverMenuInicio()
    {
        pausarDespausar();
        LoadSceneController.cargarEscena("EscenaInicio");
    }
}
