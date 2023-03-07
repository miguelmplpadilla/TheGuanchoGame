using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoMovimientoController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public GameObject objetoSeguir;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        navMeshAgent.SetDestination(objetoSeguir.transform.position);
    }
}
