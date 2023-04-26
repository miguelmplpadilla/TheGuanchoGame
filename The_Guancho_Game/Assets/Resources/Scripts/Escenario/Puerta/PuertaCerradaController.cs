using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaCerradaController : MonoBehaviour
{
    public bool cerrada = true;
    private PuertaController puertaController;

    [SerializeField] private BoxCollider boxCollider;
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        if (cerrada)
        {
            abrirCerrarPuerta(false);
        }
    }

    public void abrirCerrarPuerta(bool closed)
    {
        boxCollider2D.isTrigger = closed;
        boxCollider.enabled = closed;
    }
}
