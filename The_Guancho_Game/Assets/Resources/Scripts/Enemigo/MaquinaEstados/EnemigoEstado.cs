using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemigoEstado
{
    public EnemigoIA enemigoIa;

    public void inicializarVariables(EnemigoIA e)
    {
        enemigoIa = e;
    }
    
    public enum ESTADO
    {
        VIJILAR, ATACARESPADA, MUERTO, ATACARPISTOLA, ATACARESCOPETA
    };

    // 'EVENTOS' - En que parte nos encontramos del estado
    public enum EVENTO
    {
        ENTRAR, ACTUALIZAR, SALIR
    };

    public ESTADO nombre; // Para guardar el nombre del estado
    public EVENTO faseActual; // Para guardar la fase en la que nos encontramos
    public EnemigoEstado siguienteEstado; // El estado que se EJECUTAR? A CONTINUACI?N del estado actual

    // Constructor
    public EnemigoEstado()
    {
    }

    // Las fases de cada estado
    public virtual void Entrar() { faseActual = EVENTO.ACTUALIZAR; } // La primera fase que se ejecuta cuando cambiamos de estado. El siguiente estado deber?a ser "actualizar".
    public virtual void Actualizar() { faseActual = EVENTO.ACTUALIZAR; } // Una vez estas en ACTUALIZAR, te quedas en ACTUALIZAR hasta que quieras cambiar de estado.
    public virtual void Salir() { faseActual = EVENTO.SALIR; } // La fase de SALIR es la ?ltima antes de cambiar de ESTADO, aqu? deberiamos limpiar lo que haga falta.

    // Este es la funci?n a la que llamaremos para que el NPC inicie la m?quina de estados. Vincula los EVENTOS con las funciones que ejecuta cada uno
    public EnemigoEstado Procesar()
    {
        if (faseActual == EVENTO.ENTRAR) Entrar();
        if (faseActual == EVENTO.ACTUALIZAR) Actualizar();
        if (faseActual == EVENTO.SALIR)
        {
            Salir();
            return siguienteEstado; // IMPORTANTE: Aqu? hacemos el cambio de estado.
        }
        return this; // Si no salimos por el return de arriba, seguimos en el mismo estado.
    }

}

