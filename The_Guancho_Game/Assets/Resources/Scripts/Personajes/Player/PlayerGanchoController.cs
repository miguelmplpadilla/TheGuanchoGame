using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGanchoController : MonoBehaviour
{
    public float ganchoSpeed = 1;
    private float ganchoSpeedInicio = 1;
    public float ganchoFuerza = 2;

    private GameObject puntoAnclaje;
    private GameObject indicadorLanzarGancho;
    private PlayerMovement playerMovement;
    
    public bool ganchoDisparado = false;
    public bool puedeDisparar = true;
    
    private Rigidbody2D rigidbody;

    private BoxCollider2D boxCollider;
    public DistanceJoint2D distanceJoint;

    public List<GameObject> puntosAnclaje;

    private Vector2 direccionDisparargancho;

    public float distanciaGanchoInicio = 0;
    public float velocidadGancho = 0;

    public float fuerzaSalidaGancho = 0;

    private CameraController cameraController;

    public GameObject gancho;
    public bool ganchoEnganchado = false;
    public float speedDisparoGancho = 2;

    private float grvityScaleInicio;

    public string tipoEnganche = "";

    [SerializeField] private GameObject posicionAgarreGancho;

    private void Awake()
    {
        ganchoSpeedInicio = ganchoSpeed;
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerMovement = GetComponent<PlayerMovement>();
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
        if (!ganchoDisparado)
        {
            float anclajeCercano = 100000;
            foreach (var anclaje in puntosAnclaje)
            {
                float distancia = Vector2.Distance(transform.position, anclaje.transform.position);
                
                if (distancia < anclajeCercano)
                {
                    puntoAnclaje = anclaje;
                    anclajeCercano = distancia;
                    
                    /*if (transform.localScale.x > 0)
                    {
                        if (anclaje.transform.position.x > transform.position.x)
                        {
                            puntoAnclaje = anclaje;
                            anclajeCercano = distancia;
                        }
                    }
                    else
                    {
                        if (anclaje.transform.position.x > transform.position.x)
                        {
                            puntoAnclaje = anclaje;
                            anclajeCercano = distancia;
                        }
                    }*/
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

                if (puedeDisparar)
                {
                    if (Input.GetButtonDown("Fire2"))
                    {
                        playerMovement.animator.SetBool("swinging", true);
                        playerMovement.animator.SetTrigger("swing");
                        
                        distanciaGanchoInicio = Vector2.Distance(transform.position, puntoAnclaje.transform.position);
                        if (!puntoAnclaje.GetComponent<PuntoAnclajeScript>().tipoEnganche.Equals("balanceo"))
                        {
                            playerMovement.mov = false;
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
                /*if (playerMovement.movement.x < 0)
                {
                    rigidbody.AddForce(Vector2.left * 5, ForceMode2D.Impulse);
                }
                else if (playerMovement.movement.x > 0) 
                {
                    rigidbody.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
                }*/

                if (puntoAnclaje.GetComponent<PuntoAnclajeScript>().tipoEnganche.Equals("enemigo"))
                {
                    puntosAnclaje.Remove(puntoAnclaje);
                    puntoAnclaje.SendMessage("hit", 1);
                    cameraController.shakeCamera(0.2f,3);
                }
                
                playerMovement.animator.SetBool("swinging", false);

                rigidbody.gravityScale = grvityScaleInicio;
                playerMovement.mov = true;
                ganchoDisparado = false;
                ganchoEnganchado = false;
                gancho.transform.parent = posicionAgarreGancho.transform;
                gancho.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
        
        gancho.GetComponent<LineRenderer>().SetPosition(0, gancho.transform.position);
        gancho.GetComponent<LineRenderer>().SetPosition(1, posicionAgarreGancho.transform.position);
    }

    private void FixedUpdate()
    {
        if (!ganchoDisparado)
        {
            if (puntoAnclaje != null)
            {
                if (puedeDisparar)
                {
                    direccionDisparargancho = new Vector2(puntoAnclaje.transform.position.x - transform.position.x,
                        puntoAnclaje.transform.position.y - transform.position.y);

                    RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direccionDisparargancho, 100, 1 << 9);

                    Debug.DrawRay(transform.position, direccionDisparargancho, Color.red);

                    if (hitInfo.collider != null && hitInfo.collider.tag.Equals("PuntoAnclaje"))
                    {
                        puedeDisparar = true;
                    }
                    else
                    {
                        puedeDisparar = false;
                        indicadorLanzarGancho.GetComponent<Renderer>().material.color = Color.red;
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
        playerMovement.mov = true;
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
        } else if (puntoAnclajeScript.tipoEnganche.Equals("balanceo"))
        {
            Vector3 dir = puntoAnclajeScript.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
            
            distanceJoint.enabled = true;
            distanceJoint.connectedBody = puntoAnclaje.GetComponent<Rigidbody2D>();
            
            playerMovement.animator.SetFloat("swingHorizontalSpeed", rigidbody.velocity.x);

            if (Input.GetButtonDown("Jump"))
            {
                transform.rotation = Quaternion.Euler(0,0,0);
                    
                playerMovement.animator.SetBool("swinging", false);
                distanceJoint.enabled = false;
                rigidbody.velocity = rigidbody.velocity / 3;
                
                playerMovement.saltar();
                
                rigidbody.gravityScale = grvityScaleInicio;
                playerMovement.mov = true;
                ganchoDisparado = false;
                ganchoEnganchado = false;
                gancho.transform.parent = posicionAgarreGancho.transform;
                gancho.transform.localPosition = new Vector3(0, 0, 0);
            }
        }

        tipoEnganche = puntoAnclajeScript.tipoEnganche;
    }
}
