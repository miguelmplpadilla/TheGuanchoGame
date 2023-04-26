using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaController : MonoBehaviour
{
    public GameObject puerta;
    [SerializeField] private VariablesPlayer variablesPlayer;

    [SerializeField] private BoxCollider2D boxCollider;
    private PuertaCerradaController puertaCerradaController;

    private bool abierta = false;
    [SerializeField] private bool cerradaLlave = true;

    private void Awake()
    {
        puertaCerradaController = GetComponentInParent<PuertaCerradaController>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!puertaCerradaController.cerrada)
        {
            StartCoroutine("cerrarPuerta");
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!puertaCerradaController.cerrada)
        {
            if (!col.CompareTag("Kunai"))
            {
                boxCollider.enabled = false;
                StopCoroutine("cerrarPuerta");
            }
        }
        else
        {
            if (!abierta)
            {
                if (variablesPlayer.llaves > 0 && cerradaLlave)
                {
                    puertaCerradaController.cerrada = false;
                    puertaCerradaController.abrirCerrarPuerta(true);
                    variablesPlayer.restarLlaves(1);
                    abierta = true;
                }
            }
        }
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
        
        boxCollider.enabled = true;
    }
}