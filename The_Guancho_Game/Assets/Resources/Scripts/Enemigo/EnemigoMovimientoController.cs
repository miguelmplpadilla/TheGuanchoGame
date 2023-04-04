using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoMovimientoController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    private EnemigoHitController enemigoHitController;

    public GameObject objetoSeguir;
    private GameObject player;
    public GameObject modelo;

    [SerializeField] private GameObject[] puntosPatrulla;
    private GameObject puntoPatrullaActual;
    private int contadorPatrulla = 0;

    public bool atacando = false;

    private float speedOriginal;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemigoHitController = GetComponentInChildren<EnemigoHitController>();

        speedOriginal = navMeshAgent.speed;
    }

    private void Start()
    {
        player = GameObject.Find("Player");

        puntoPatrullaActual = puntosPatrulla[contadorPatrulla];
    }

    void Update()
    {
        if (!enemigoHitController.muerto && !player.GetComponentInChildren<PlayerHurtController>().muerto)
        {
            if (objetoSeguir.transform.position.x > transform.position.x)
            {
                modelo.transform.rotation = Quaternion.Euler(0,90,0);
            }
            else
            {
                modelo.transform.rotation = Quaternion.Euler(0,-90,0);
            }
            
            float distanciaPlayer = Vector2.Distance(player.transform.position, transform.position);
            float distanciaVerticalPlayer = Vector2.Distance(new Vector2(transform.position.x, player.transform.position.y),
                transform.position);

            if (distanciaPlayer < 15 && distanciaVerticalPlayer < 3)
            {
                if (!atacando)
                {
                    navMeshAgent.speed = speedOriginal;
                    objetoSeguir = player;

                    if (distanciaPlayer < 2)
                    {
                        StartCoroutine("pararAtacar");
                        animator.SetTrigger("atacar");
                        animator.SetBool("run", false);
                        atacando = true;
                    }
                }
                else
                {
                    navMeshAgent.speed = 0;
                }
            }
            else
            {
                navMeshAgent.speed = speedOriginal;
                objetoSeguir = puntoPatrullaActual;
            }
        }
        else
        {
            StopCoroutine("pararAtacar");
            navMeshAgent.speed = 0;
        }
        
        Vector3 seguimientoObeto = new Vector3(objetoSeguir.transform.position.x, objetoSeguir.transform.position.y,
            transform.position.z);
        navMeshAgent.SetDestination(seguimientoObeto);
    }

    private void LateUpdate()
    {
        if (!objetoSeguir.Equals(player))
        {
            float distanciaPuntoPatrulla = Vector2.Distance(transform.position, puntoPatrullaActual.transform.position);

        
            if (distanciaPuntoPatrulla < 1)
            {
                contadorPatrulla++;
            
                if (contadorPatrulla == puntosPatrulla.Length)
                {
                    contadorPatrulla = 0;
                }
            
                puntoPatrullaActual = puntosPatrulla[contadorPatrulla];
            }
        }
    }

    private IEnumerator pararAtacar()
    {
        yield return new WaitForSeconds(2f);
        atacando = false;
        animator.SetBool("run", true);
    }
}