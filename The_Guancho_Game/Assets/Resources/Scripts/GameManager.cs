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
        Debug.Log("VidaPlayer: "+variablesPlayer.vida);
        Debug.Log("KunaisPlayer: "+variablesPlayer.kunais);
        
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
        variablesPlayer.actualizarKunais();
        variablesPlayer.actualizarVida();
    }
}
