using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaMuerteController : MonoBehaviour
{
    public void reintentar()
    {
        LoadSceneController.cargarEscena(PlayerPrefs.GetString("EscenaAnterior"));
    }
}
