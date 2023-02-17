using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuarController : MonoBehaviour
{
    public bool interactuando = false;
    public bool puedeInteractuar = false;

    private GameObject objetoInteractuable;

    [SerializeField]
    private string botonInteractuar = "Interactuar";
    
    void Update()
    {
        if (puedeInteractuar)
        {
            if (!interactuando)
            {
                if (Input.GetButtonDown(botonInteractuar))
                {
                    objetoInteractuable.SendMessage("inter", gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Inter"))
        {
            botonInteractuar = col.GetComponent<ObjetoInteractuableController>().botonInteractuar;
            col.SendMessage("interEnter");
            objetoInteractuable = col.gameObject;
            puedeInteractuar = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Inter"))
        {
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Inter"))
        {
            other.SendMessage("interExit");
            objetoInteractuable = null;
            puedeInteractuar = false;
            interactuando = false;
        }
    }
}
