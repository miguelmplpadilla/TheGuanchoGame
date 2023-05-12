using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoIA: MonoBehaviour
{
    public string arma;

    public bool atacando = false;
    public bool muerto = false;
    
    public EnemigoEstado FSM;

    [SerializeField] private LayerMask layerInteract;

    [NonSerialized] public GameObject player;
    private GameObject puntoMultiUsos;
    
    [NonSerialized] public NavMeshAgent navMeshAgent;
    [NonSerialized] public Animator animator;

    public float velocidad = 2;
    public float velocidadAtaque = 3;
    [SerializeField] private float distanciaDeteccion = 10;

    public GameObject punto1;
    public GameObject punto2;

    void Start()
    {
        player = GameObject.Find("Player");
        puntoMultiUsos = transform.Find("PuntoMultiUsos").gameObject;
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = transform.GetChild(0).GetComponentInChildren<Animator>();

        FSM = new EnemigoVigilar();
        FSM.inicializarVariables(this);
    }

    void Update()
    {
        FSM = FSM.Procesar(); // INICIAMOS LA FSM
    }

    public void destruirObjeto(GameObject objetoDestruir)
    {
        Destroy(objetoDestruir);
    }
    
    public bool playerCerca()
    {
        bool cerca = false;

        float distancia = Vector3.Distance(transform.position, player.transform.position);
        
        if (distancia < distanciaDeteccion)
        {
            cerca = true;
        }

        return cerca;
    }
    
    public bool puedeVerJugador()
    {
        bool puedeVer = false;
        
        Vector3 direccion = (player.transform.position - puntoMultiUsos.transform.position);

        RaycastHit2D hit = Physics2D.Raycast(puntoMultiUsos.transform.position, direccion, Mathf.Infinity, layerInteract);
        
        if (hit.transform.CompareTag("Player"))
        {
            puedeVer = true;
            Debug.DrawRay(puntoMultiUsos.transform.position, direccion, Color.green);
        }
        else
        {
            Debug.DrawRay(puntoMultiUsos.transform.position, direccion, Color.red);
        }
        
        return puedeVer;
    }

    public void direccionMirar(GameObject objetoMirar)
    {
        if (transform.position.x > objetoMirar.transform.position.x)
        {
            transform.GetChild(0).rotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            transform.GetChild(0).rotation = Quaternion.Euler(0, 90, 0);
        }
    }
}