using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicaInicioController : MonoBehaviour
{
    [SerializeField] private GameObject timeline1;
    [SerializeField] private GameObject timeline2;
    [SerializeField] private GameObject timeline3;
    
    [SerializeField] private GameObject[] objetosDesactivarTimeline1;
    [SerializeField] private GameObject[] objetosDesactivarTimeline2;

    public void activarTimeline2()
    {
        timeline1.SetActive(false);
        
        for (int i = 0; i < objetosDesactivarTimeline1.Length; i++)
        {
            objetosDesactivarTimeline1[i].SetActive(false);
        }
        
        timeline2.SetActive(true);
    }
    
    public void activarTimeline3()
    {
        timeline2.SetActive(false);
        
        for (int i = 0; i < objetosDesactivarTimeline2.Length; i++)
        {
            objetosDesactivarTimeline2[i].SetActive(false);
        }
        
        timeline3.SetActive(true);
    }

    public void moverEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
