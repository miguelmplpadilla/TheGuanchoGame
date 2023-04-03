using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoAnimatorExecuteController : MonoBehaviour
{
    private EnemigoMovimientoController enemigoMovimientoController;

    private void Awake()
    {
        enemigoMovimientoController = GetComponentInParent<EnemigoMovimientoController>();
    }

    public void setAtacandoFalse()
    {
        enemigoMovimientoController.atacando = false;
    }
}
