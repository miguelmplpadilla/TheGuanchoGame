using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class NaveController : MonoBehaviour
{

    [SerializeField] private VideoPlayer video;

    [SerializeField] private string nombreEscena = "";

    public void moverEscena()
    {
        SceneManager.LoadScene(nombreEscena);
    }

    public void iniciarCinematica()
    {
        video.Play();

        Invoke("moverEscena", (float)video.clip.length);
    }
}
