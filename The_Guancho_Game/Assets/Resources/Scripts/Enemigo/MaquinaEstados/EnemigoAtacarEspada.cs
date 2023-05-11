using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Constructor para VIGILAR
public class EnemigoAtacarEspada : EnemigoEstado
{
    public EnemigoAtacarEspada() : base()
    {
        Debug.Log("Atacar");
        
        nombre = ESTADO.ATACAR; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entrar()
    {
        enemigoIa.navMeshAgent.speed = enemigoIa.velocidad;
        
        base.Entrar();
    }

    public override void Actualizar()
    {
        if (!enemigoIa.atacando)
        {
            float distanciaJugador = Vector2.Distance(enemigoIa.transform.position, enemigoIa.player.transform.position);

            if (distanciaJugador < 1)
            {
                enemigoIa.navMeshAgent.speed = 0;
            
                enemigoIa.animator.SetTrigger("atacar");

                enemigoIa.atacando = true;
            }
            else
            {
                if (enemigoIa.puedeVerJugador() && enemigoIa.playerCerca())
                {
                    enemigoIa.navMeshAgent.SetDestination(enemigoIa.player.transform.position);
                }
                else
                {
                    siguienteEstado = new EnemigoVigilar();
                    siguienteEstado.inicializarVariables(enemigoIa);

                    faseActual = EVENTO.SALIR;
                }
            }
        }
    }

    public override void Salir()
    {
        base.Salir();
    }
}

