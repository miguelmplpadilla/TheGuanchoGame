using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject[] listaTutoriales;

    private int numTutorialMostrar = 0;

    public string escenaCargar;

    private void Start()
    {
        mostrarSiguienteTutorial();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            mostrarSiguienteTutorial();
        }
    }

    private void mostrarSiguienteTutorial()
    {
        listaTutoriales[numTutorialMostrar].SetActive(true);

        if (numTutorialMostrar != 0)
        {
            listaTutoriales[numTutorialMostrar-1].SetActive(false);
        }

        numTutorialMostrar++;

        if (numTutorialMostrar == listaTutoriales.Length)
        {
            LoadSceneController.cargarEscena(escenaCargar);
        }
    }
}