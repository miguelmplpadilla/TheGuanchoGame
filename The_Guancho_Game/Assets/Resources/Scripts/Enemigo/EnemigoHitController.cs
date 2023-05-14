using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemigoHitController : MonoBehaviour
{
    public float vida = 1;

    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;
    private PlayerGanchoController playerGanchoController;
    
    [SerializeField] private Animator animator;
    private EnemigoIA enemigoIa;

    [SerializeField] private GameObject[] objetosCrear;
    [SerializeField] private GameObject puntoCrearObjeto;

    public bool muerto = false;

    private void Awake()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        audioSource = GetComponentInParent<AudioSource>();

        enemigoIa = GetComponentInParent<EnemigoIA>();
    }

    private void Start()
    {
        playerGanchoController = GameObject.Find("Player").GetComponent<PlayerGanchoController>();
    }

    public void hit(float dano)
    {
        if (!muerto)
        {
            if (vida > 0)
            {
                vida -= dano;
            }

            if (vida <= 0)
            {
                GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                GetComponentInParent<CapsuleCollider2D>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                Instantiate(objetosCrear[Random.Range(0, objetosCrear.Length)], puntoCrearObjeto.transform.position, Quaternion.identity);
                
                animator.SetBool("muerto", true);
                animator.SetTrigger("morir");
                navMeshAgent.speed = 0;

                playerGanchoController.puntosAnclaje.Remove(transform.parent.gameObject);

                enemigoIa.morir();
                
                muerto = true;
            }
        }
    }
}
