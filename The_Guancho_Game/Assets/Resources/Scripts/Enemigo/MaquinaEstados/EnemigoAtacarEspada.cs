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
        
        nombre = ESTADO.ATACARESPADA; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entrar()
    {
        enemigoIa.hitBox.SetActive(true);
        enemigoIa.navMeshAgent.speed = enemigoIa.velocidad;

        base.Entrar();
    }

    public override void Actualizar()
    {
        if (!enemigoIa.atacando)
        {
            float distanciaJugador = Vector2.Distance(enemigoIa.transform.position, enemigoIa.player.transform.position);

            if (distanciaJugador < enemigoIa.distanciaAtacarPlayer)
            {
                enemigoIa.rigidbody.sharedMaterial = enemigoIa.fullFriction;
                
                enemigoIa.hitBox.SetActive(true);
                
                enemigoIa.navMeshAgent.speed = 0;
                
                enemigoIa.animator.SetBool("run", false);
            
                enemigoIa.animator.SetTrigger("atacar");

                enemigoIa.atacando = true;
            }
            else
            {
                if (enemigoIa.puedeVerJugador() && enemigoIa.playerCerca())
                {
                    enemigoIa.rigidbody.sharedMaterial = null;

                    Debug.Log(enemigoIa.velocidadAtaque);
                    
                    enemigoIa.navMeshAgent.speed = enemigoIa.velocidadAtaque;
                    enemigoIa.animator.SetBool("run", true);
                    
                    enemigoIa.navMeshAgent.SetDestination(enemigoIa.player.transform.position);
                }
                else
                {
                    enemigoIa.rigidbody.sharedMaterial = null;
                    
                    siguienteEstado = new EnemigoVigilar();
                    siguienteEstado.inicializarVariables(enemigoIa);

                    faseActual = EVENTO.SALIR;
                }
            }
            
            enemigoIa.direccionMirar(enemigoIa.player);
        }
    }

    public override void Salir()
    {
        enemigoIa.hitBox.SetActive(false);
        base.Salir();
    }
}

