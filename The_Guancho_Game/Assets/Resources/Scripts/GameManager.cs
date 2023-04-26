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
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("ScenePruebaScriptableObject");
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
