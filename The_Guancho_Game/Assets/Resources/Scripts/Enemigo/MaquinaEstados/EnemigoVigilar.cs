using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Constructor para VIGILAR
public class EnemigoVigilar : EnemigoEstado
{
    private GameObject puntoMover;
    private int numPunto = 0;

    public EnemigoVigilar() : base()
    {
        Debug.Log("Vijilando");

        nombre = ESTADO.VIJILAR;
    }

    public override void Entrar()
    {
        puntoMover = enemigoIa.puntos[numPunto];
        enemigoIa.navMeshAgent.speed = enemigoIa.velocidad;
        enemigoIa.animator.SetBool("run", true);
        base.Entrar();
    }

    public override void Actualizar()
    {
        enemigoIa.navMeshAgent.SetDestination(puntoMover.transform.position);

        float distancia = Vector3.Distance(enemigoIa.transform.position, puntoMover.transform.position);

        if (distancia < 1)
        {
            numPunto++;

            if (numPunto == enemigoIa.puntos.Length)
            {
                numPunto = 0;
            }
            
            puntoMover = enemigoIa.puntos[numPunto];
        }

        if (enemigoIa.puedeVerJugador() && enemigoIa.playerCerca())
        {
            if (enemigoIa.arma.Equals("Espada"))
            {
                siguienteEstado = new EnemigoAtacarEspada();
            }
            else if (enemigoIa.arma.Equals("Pistola"))
            {
                siguienteEstado = new EnemigoAtacarPistola();
            }
            else if (enemigoIa.arma.Equals("Escopeta"))
            {
                siguienteEstado = new EnemigoAtacarEscopeta();
            }
            
            siguienteEstado.inicializarVariables(enemigoIa);
            faseActual = EVENTO.SALIR;
        }

        enemigoIa.direccionMirar(puntoMover);
    }

    public override void Salir()
    {
        base.Salir();
    }
}