using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGanchoController : MonoBehaviour
{
    public float ganchoSpeed = 1;

    private GameObject puntoAnclaje;
    private GameObject indicadorLanzarGancho;
    private PlayerController playerController;
    private PlayerCombateController playerCombateController;
    
    public bool ganchoDisparado = false;
    public bool puedeDisparar = true;
    
    private Rigidbody2D rigidbody;

    private BoxCollider2D boxCollider;
    public DistanceJoint2D distanceJoint;

    public List<GameObject> puntosAnclaje;

    private Vector2 direccionDisparargancho;

    public float distanciaGanchoInicio = 0;
    public float velocidadGancho = 0;

    private CameraController cameraController;

    public GameObject gancho;
    public bool ganchoEnganchado = false;
    public float speedDisparoGancho = 2;

    private float grvityScaleInicio;

    public string tipoEnganche = "";

    [SerializeField] private GameObject posicionAgarreGancho;

    [SerializeField] private GameObject puntoLanzarRayCastParedes;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerController = GetComponent<PlayerController>();
        playerCombateController = GetComponent<PlayerCombateController>();
    }

    private void Start()
    {
        indicadorLanzarGancho = GameObject.Find("IndicadorLanzarGancho");
        puntosAnclaje = GameObject.FindGameObjectsWithTag("PuntoAnclaje").ToList();
        cameraController = GameObject.Find("CM").GetComponent<CameraController>();

        grvityScaleInicio = rigidbody.gravityScale;
    }

    void Update()
    {
        if (!ganchoDisparado && !playerCombateController.isKunaiLanzado)
        {
            float anclajeCercano = 100000;
            foreach (var anclaje in puntosAnclaje)
            {
                float distancia = Vector2.Distance(transform.position, anclaje.transform.position);
                
                if (distancia < anclajeCercano)
                {
                    puntoAnclaje = anclaje;
                    anclajeCercano = distancia;
                }
            }

            float distanciaPuntoAnclaje = 0;
            
            if (puntoAnclaje != null)
            {
                distanciaPuntoAnclaje = Vector2.Distance(transform.position, puntoAnclaje.transform.position);
                
                if (distanciaPuntoAnclaje <= 15)
                {
                    indicadorLanzarGancho.GetComponent<Renderer>().material.color = Color.white;
                    indicadorLanzarGancho.transform.position = new Vector3(puntoAnclaje.transform.position.x, puntoAnclaje.transform.position.y, 
                        indicadorLanzarGancho.transform.position.z);
                    puedeDisparar = true;
                }
                else
                {
                    indicadorLanzarGancho.GetComponent<Renderer>().material.color = Color.red;
                    puedeDisparar = false;
                }
                
                comprobarRayCast();

                if (puedeDisparar)
                {
                    if (Input.GetButtonDown("Fire2"))
                    {
                        playerCombateController.isKunaiLanzado = false;
                        playerController.animator.SetBool("swinging", true);
                        playerController.animator.SetTrigger("swing");
                        
                        distanciaGanchoInicio = Vector2.Distance(transform.position, puntoAnclaje.transform.position);
                        if (!puntoAnclaje.GetComponent<PuntoAnclajeScript>().tipoEnganche.Equals("balanceo"))
                        {
                            playerController.mov = false;
                        }

                        StartCoroutine(moverGanchoAEnganche());
                        ganchoDisparado = true;
                    }
                }
                
                if (!puedeDisparar)
                {
                    indicadorLanzarGancho.GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    indicadorLanzarGancho.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }

        if (ganchoEnganchado)
        {
            float distancia = Vector3.Distance(transform.position, puntoAnclaje.transform.position);
            if (distancia > 2f)
            {
                lanzarGanchoAnclaje();
            }
            else
            {
                if (puntoAnclaje.GetComponent<PuntoAnclajeScript>().tipoEnganche.Equals("enemigo"))
                {
                    puntosAnclaje.Remove(puntoAnclaje);
                    puntoAnclaje.BroadcastMessage("hit", 1);
                    cameraController.shakeCamera(0.2f,3);
                }
                
                playerController.animator.SetBool("swinging", false);

                rigidbody.gravityScale = grvityScaleInicio;
                playerController.mov = true;
                ganchoDisparado = false;
                ganchoEnganchado = false;
                gancho.transform.parent = posicionAgarreGancho.transform;
                gancho.transform.localPosition = new Vector3(0, 0, 0);
                transform.rotation = quaternion.Euler(0,0,0);
            }
        }
        
        gancho.GetComponent<LineRenderer>().SetPosition(0, gancho.transform.position);
        gancho.GetComponent<LineRenderer>().SetPosition(1, posicionAgarreGancho.transform.position);
    }

    private void comprobarRayCast()
    {
        if (!ganchoDisparado)
        {
            if (puntoAnclaje != null)
            {
                if (puedeDisparar)
                {
                    Vector2 direccionDispararRayCastParedes = new Vector2(
                        puntoAnclaje.transform.position.x - puntoLanzarRayCastParedes.transform.position.x,
                        puntoAnclaje.transform.position.y - puntoLanzarRayCastParedes.transform.position.y);

                    RaycastHit2D hitInfoParedes = Physics2D.Raycast(puntoLanzarRayCastParedes.transform.position,
                        direccionDispararRayCastParedes, Vector3.Distance(puntoAnclaje.transform.position, puntoLanzarRayCastParedes.transform.position), 1 << 8);

                    Debug.DrawRay(puntoLanzarRayCastParedes.transform.position, direccionDispararRayCastParedes,
                        Color.red);

                    if (hitInfoParedes.collider != null && hitInfoParedes.collider.CompareTag("Suelo"))
                    {
                        puedeDisparar = false;
                        indicadorLanzarGancho.GetComponent<Renderer>().material.color = Color.red;
                    }
                    else
                    {
                        puedeDisparar = true;
                    }
                }
            }
        }
    }

    private IEnumerator moverGanchoAEnganche()
    {
        gancho.transform.parent = null;
        while (true)
        {
            gancho.transform.position = Vector2.MoveTowards(gancho.transform.position, puntoAnclaje.transform.position, speedDisparoGancho * Time.deltaTime);
            float distancia = Vector2.Distance(gancho.transform.position, puntoAnclaje.transform.position);
            if (distancia < 0.1f)
            {
                ganchoEnganchado = true;
                break;
            }
            
            yield return null;
        }

        yield return null;
    }

    private IEnumerator putMovFalse()
    {
        yield return new WaitForSeconds(0.2f);
        rigidbody.gravityScale = grvityScaleInicio;
        playerController.mov = true;
        ganchoDisparado = false;
    }

    public void lanzarGanchoAnclaje()
    {
        PuntoAnclajeScript puntoAnclajeScript = puntoAnclaje.GetComponent<PuntoAnclajeScript>();
        
        if (puntoAnclajeScript.tipoEnganche.Equals("enganche") || puntoAnclajeScript.tipoEnganche.Equals("enemigo"))
        {
            rigidbody.gravityScale = 0;
            rigidbody.velocity = Vector2.zero;

            if (!puntoAnclaje.GetComponent<PuntoAnclajeScript>().tipoEnganche.Equals("enemigo"))
            {
                float distanciaGancho = Vector2.Distance(puntoAnclaje.transform.position, transform.position);

                velocidadGancho = (ganchoSpeed * distanciaGancho) / distanciaGanchoInicio;
            }
            else
            {
                velocidadGancho = ganchoSpeed;
            }

            transform.position = Vector3.MoveTowards(transform.position, puntoAnclaje.transform.position, velocidadGancho * Time.deltaTime);
            
            Vector3 dir = puntoAnclaje.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
            
        } else if (puntoAnclajeScript.tipoEnganche.Equals("balanceo"))
        {
            Vector3 dir = puntoAnclajeScript.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
            
            distanceJoint.enabled = true;
            distanceJoint.connectedBody = puntoAnclaje.GetComponent<Rigidbody2D>();
            
            playerController.animator.SetFloat("swingHorizontalSpeed", rigidbody.velocity.x);

            if (Input.GetButtonDown("Jump"))
            {
                transform.rotation = Quaternion.Euler(0,0,0);
                    
                playerController.animator.SetBool("swinging", false);
                distanceJoint.enabled = false;
                rigidbody.velocity = rigidbody.velocity / 3;
                
                playerController.jump();
                
                rigidbody.gravityScale = grvityScaleInicio;
                playerController.mov = true;
                ganchoDisparado = false;
                ganchoEnganchado = false;
                gancho.transform.parent = posicionAgarreGancho.transform;
                gancho.transform.localPosition = new Vector3(0, 0, 0);
            }
        }

        tipoEnganche = puntoAnclajeScript.tipoEnganche;
    }
}
