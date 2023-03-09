using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaController : MonoBehaviour
{
    private GameObject puerta;

    private void Awake()
    {
        puerta = transform.parent.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine("cerrarPuerta");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        StopCoroutine("cerrarPuerta");
    }

    IEnumerator cerrarPuerta()
    {
        Quaternion rotacionActual = puerta.transform.rotation;

        float restaInicial = 0.01f;

        yield return new WaitForSeconds(1f);

        while (true)
        {
            rotacionActual = new Quaternion(rotacionActual.x - restaInicial, rotacionActual.y, rotacionActual.z, 0);

            puerta.transform.rotation = rotacionActual;
            
            yield return null;
        }
    }
}
