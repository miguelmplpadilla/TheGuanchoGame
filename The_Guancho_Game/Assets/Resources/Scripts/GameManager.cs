using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public VariablesPlayer variablesPlayer;

    private void Start()
    {
        variablesPlayer.reiniciarVariables();
        
        variablesPlayer.inicializacion();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("InicioCinematica");
        }
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("AvionCinematica");
        }
        
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("BarcoCinematica");
        }
        
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("EspacioCinematica");
        }
    }

    private void LateUpdate()
    {
        variablesPlayer.actualizarVariables();
    }

    public void reiniciarVariables()
    {
        variablesPlayer.reiniciarVariables();
    }
}
