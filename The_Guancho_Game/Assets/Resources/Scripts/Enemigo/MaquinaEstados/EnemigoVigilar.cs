using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Constructor para VIGILAR
public class EnemigoVigilar : EnemigoEstado
{
    private GameObject puntoMover;
    
    public EnemigoVigilar() : base()
    {
        Debug.Log("Vijilando");
        
        nombre = ESTADO.VIJILAR; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entrar()
    {
        puntoMover = enemigoIa.punto1;
        enemigoIa.navMeshAgent.speed = enemigoIa.velocidad;
        enemigoIa.animator.SetBool("run", true);
        base.Entrar();
    }

    public override void Actualizar()
    {
        if (!enemigoIa.puedeVerJugador() && !enemigoIa.playerCerca())
        {
            enemigoIa.navMeshAgent.SetDestination(puntoMover.transform.position);

            float distancia = Vector3.Distance(enemigoIa.transform.position, puntoMover.transform.position);
            
            if (distancia < 1)
            {
                if (puntoMover.Equals(enemigoIa.punto1))
                {
                    puntoMover = enemigoIa.punto2;
                }
                else
                {
                    puntoMover = enemigoIa.punto1;
                }
            }
        }
        else
        {
            if (enemigoIa.arma.Equals("Espada"))
            {
                siguienteEstado = new EnemigoAtacarEspada();
            }
            
            siguienteEstado.inicializarVariables(enemigoIa);
            faseActual = EVENTO.SALIR;
        }
    }

    public override void Salir()
    {
        enemigoIa.animator.SetBool("run", false);
        base.Salir();
    }
}

