using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemigoHitController : MonoBehaviour
{
    [SerializeField]
    private float vida = 1;

    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;
    private Animator animator;

    [SerializeField] private GameObject[] objetosCrear;
    [SerializeField] private GameObject puntoCrearObjeto;

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
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            Instantiate(objetosCrear[Random.Range(0, objetosCrear.Length)], puntoCrearObjeto.transform.position, Quaternion.identity);
            muerto = true;
            animator.SetBool("muerto", true);
            animator.SetTrigger("morir");
            audioSource.Play();
            navMeshAgent.speed = 0;
        }
    }
}
