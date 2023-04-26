using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VentilacionController : MonoBehaviour
{
    [SerializeField] private GameObject[] puntosVentilacion;
    private GameObject player;
    private CinemachineVirtualCamera mainCamera;

    [SerializeField] private GameObject punto1;
    [SerializeField] private GameObject punto2;

    private GameObject puntoMover;

    private void Awake()
    {
        player = GameObject.Find("Player");
        mainCamera = GameObject.Find("CM").GetComponent<CinemachineVirtualCamera>();
    }

    public void moverVentilacion(GameObject puntoEnganchado)
    {
        if (puntoEnganchado.Equals(punto1))
        {
            puntoMover = punto2;
        }
        else
        {
            puntoMover = punto1;
        }
        
        player.SetActive(false);

        StartCoroutine(movimientoCamara());
    }

    private IEnumerator movimientoCamara()
    {
        for (int i = 0; i < puntosVentilacion.Length; i++)
        {
            mainCamera.Follow = puntosVentilacion[i].transform;
            yield return new WaitForSeconds(0.5f);
        }

        player.SetActive(true);
        player.transform.position = puntoMover.transform.position;
        mainCamera.Follow = player.transform;

        yield return null;
    }
}
