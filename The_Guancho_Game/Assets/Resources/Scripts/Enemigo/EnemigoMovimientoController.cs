using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoMovimientoController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    public GameObject objetoSeguir;
    private GameObject player;

    [SerializeField] private GameObject[] puntosPatrulla;
    private GameObject puntoPatrullaActual;
    private int contadorPatrulla = 0;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        player = GameObject.Find("Player");

        puntoPatrullaActual = puntosPatrulla[contadorPatrulla];
    }

    void Update()
    {
        float distanciaPlayer = Vector2.Distance(player.transform.position, transform.position);
        float distanciaVerticalPlayer = Vector2.Distance(new Vector2(transform.position.x, player.transform.position.y),
            transform.position);

        if (distanciaPlayer < 15 && distanciaVerticalPlayer < 3)
        {
            objetoSeguir = player;
        }
        else
        {
            objetoSeguir = puntoPatrullaActual;
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

            Debug.Log("Distancia punto patrulla: "+distanciaPuntoPatrulla);
        
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
}