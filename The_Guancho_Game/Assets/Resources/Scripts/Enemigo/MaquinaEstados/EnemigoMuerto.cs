using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Constructor para VIGILAR
public class EnemigoMuerto : EnemigoEstado
{
    public EnemigoMuerto() : base()
    {
        nombre = ESTADO.MUERTO;
    }

    public override void Entrar()
    {
        base.Entrar();
    }

    public override void Actualizar()
    {
    }

    public override void Salir()
    {
        base.Salir();
    }
}

