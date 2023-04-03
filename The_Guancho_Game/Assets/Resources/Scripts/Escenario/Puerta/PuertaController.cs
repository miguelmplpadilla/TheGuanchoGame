using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaController : MonoBehaviour
{
    public GameObject puerta;

    private void OnTriggerExit2D(Collider2D other)
    {
        StartCoroutine("cerrarPuerta");
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        StopCoroutine("cerrarPuerta");
    }

    IEnumerator cerrarPuerta()
    {
        Vector3 rotacionActual = puerta.transform.rotation.eulerAngles;

        float restaInicial = 1f;

        if (puerta.transform.rotation.y < 0)
        {
            restaInicial = -1f;
        }

        yield return new WaitForSeconds(1f);

        while (true)
        {
            rotacionActual = new Vector3(rotacionActual.x, rotacionActual.y - restaInicial, rotacionActual.z);

            puerta.transform.rotation = Quaternion.Euler(rotacionActual);
            
            if (puerta.transform.rotation.y <= 0)
            {
                rotacionActual = new Vector3(rotacionActual.x, 0, rotacionActual.z);
                puerta.transform.rotation = Quaternion.Euler(rotacionActual);
                break;
            }

            yield return null;
        }
    }
}