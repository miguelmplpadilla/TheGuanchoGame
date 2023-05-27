using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NaveController : MonoBehaviour
{
    public void moverEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
