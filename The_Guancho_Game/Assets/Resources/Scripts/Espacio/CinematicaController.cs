using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicaController : MonoBehaviour
{
    [SerializeField] private GameObject naveInicial;
    [SerializeField] private GameObject timelineInicial;
    [SerializeField] private GameObject camaraInicio;
    
    [SerializeField] private GameObject naveFinal;
    [SerializeField] private GameObject timelineFinal;
    
    public void empezarCinematicaFinal()
    {
        camaraInicio.SetActive(false);
        naveInicial.SetActive(false);
        timelineInicial.SetActive(false);
        
        naveFinal.SetActive(true);
        timelineFinal.SetActive(true);
    }
}
