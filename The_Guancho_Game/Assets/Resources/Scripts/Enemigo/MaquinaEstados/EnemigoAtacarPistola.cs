using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Constructor para VIGILAR
public class EnemigoAtacarPistola : EnemigoEstado
{
    public EnemigoAtacarPistola() : base()
    {
        Debug.Log("Atacar");
        
        nombre = ESTADO.ATACARPISTOLA; // Guardamos el nombre del estado en el que nos encontramos.
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

            if (distanciaJugador < 10)
            {
                enemigoIa.navMeshAgent.speed = 0;
                
                enemigoIa.animator.SetBool("run", false);
            
                enemigoIa.animator.SetTrigger("atacarPistola");
                
                if (enemigoIa.transform.position.x > enemigoIa.player.transform.position.x)
                {
                    enemigoIa.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    enemigoIa.transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
                }

                enemigoIa.enemigoAnimatorExecuteController.direccionDisparar = enemigoIa.player.transform.position;

                enemigoIa.atacando = true;
            }
            else
            {
                if (enemigoIa.puedeVerJugador() && enemigoIa.playerCerca())
                {
                    enemigoIa.navMeshAgent.speed = enemigoIa.velocidadAtaque;
                    enemigoIa.animator.SetBool("run", true);
                    enemigoIa.navMeshAgent.speed = enemigoIa.velocidad;
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

