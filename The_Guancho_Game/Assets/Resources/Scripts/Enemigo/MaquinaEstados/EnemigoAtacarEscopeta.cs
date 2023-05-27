using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Constructor para VIGILAR
public class EnemigoAtacarEscopeta : EnemigoEstado
{
    public EnemigoAtacarEscopeta() : base()
    {
        Debug.Log("Atacar");
        
        nombre = ESTADO.ATACARESCOPETA; // Guardamos el nombre del estado en el que nos encontramos.
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
            
            enemigoIa.direccionMirar(enemigoIa.player);

            if (distanciaJugador < enemigoIa.distanciaAtacarPlayer)
            {
                if (enemigoIa.puedeVerJugador() && enemigoIa.playerCerca())
                {
                    enemigoIa.rigidbody.sharedMaterial = enemigoIa.fullFriction;
                
                    enemigoIa.navMeshAgent.speed = 0;
                
                    enemigoIa.animator.SetBool("run", false);
                    enemigoIa.animator.SetTrigger("atacarEscopeta");

                    enemigoIa.enemigoAnimatorExecuteController.direccionDisparar = enemigoIa.player.transform.position;

                    enemigoIa.atacando = true;
                }
                else
                {
                    enemigoIa.rigidbody.sharedMaterial = null;
                    
                    siguienteEstado = new EnemigoVigilar();
                    siguienteEstado.inicializarVariables(enemigoIa);

                    faseActual = EVENTO.SALIR;
                }
            }
            else
            {
                if (enemigoIa.puedeVerJugador() && enemigoIa.playerCerca())
                {
                    enemigoIa.navMeshAgent.speed = enemigoIa.velocidadAtaque;
                    enemigoIa.navMeshAgent.SetDestination(enemigoIa.player.transform.position);
                    
                    enemigoIa.rigidbody.sharedMaterial = null;
                    enemigoIa.animator.SetBool("run", true);
                }
                else
                {
                    enemigoIa.rigidbody.sharedMaterial = null;
                    
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

