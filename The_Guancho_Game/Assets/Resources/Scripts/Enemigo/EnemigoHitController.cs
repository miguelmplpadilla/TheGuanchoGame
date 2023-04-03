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

    public bool muerto = false;

    private void Awake()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        audioSource = GetComponentInParent<AudioSource>();
        animator = transform.parent.GetComponentInChildren<Animator>();
    }

    public void hit(float dano)
    {
        if (vida > 0)
        {
            vida -= dano;
        }

        if (vida <= 0)
        {
            muerto = true;
            animator.SetTrigger("morir");
            audioSource.Play();
            navMeshAgent.speed = 0;
        }
    }
}
