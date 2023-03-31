using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoHitController : MonoBehaviour
{
    [SerializeField]
    private float vida = 1;

    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }

    public void hit(float dano)
    {
        if (vida > 0)
        {
            vida -= dano;
        }

        if (vida <= 0)
        {
            // Aqui va la animacion de morir y despues se destruye
            StartCoroutine("destruirEnemigo");
        }
    }

    IEnumerator destruirEnemigo()
    {
        audioSource.Play();
        navMeshAgent.speed = 0;
        
        yield return new WaitForSeconds(0.9f);
        
        Destroy(gameObject);
    }
}
