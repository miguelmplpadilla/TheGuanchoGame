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
    private Animator animator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
    }

    public void hit(float dano)
    {
        if (vida > 0)
        {
            vida -= dano;
        }

        if (vida <= 0)
        {
            animator.SetTrigger("morir");
            audioSource.Play();
            navMeshAgent.speed = 0;
        }
    }
}
