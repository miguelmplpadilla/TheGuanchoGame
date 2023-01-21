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
    
    private bool ganchoDisparado = false;
    private bool puedeDisparar = true;
    
    private Rigidbody2D rigidbody;

    private BoxCollider2D boxCollider;
    public DistanceJoint2D distanceJoint;

    public List<GameObject> puntosAnclaje;

    private Vector2 direccionDisparargancho;

    public float distanciaGanchoInicio = 0;
    public float velocidadGancho = 0;

    public float fuerzaSalidaGancho = 0;

    private CameraController cameraController;

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
        cameraController = GameObject.Find("CM vcam1").GetComponent<CameraController>();
    }

    void Update()
    {
        if (puntoAnclaje == null)
        {
            indicadorLanzarGancho.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            indicadorLanzarGancho.GetComponent<MeshRenderer>().enabled = true;
        }
        
        if (!ganchoDisparado)
        {
            float anclajeCercano = 1000;
            foreach (var anclaje in puntosAnclaje)
            {
                float distancia = Vector2.Distance(transform.position, anclaje.transform.position);
                if (distancia < anclajeCercano)
                {
                    if (transform.localScale.x > 0)
                    {
                        if (anclaje.transform.position.x > transform.position.x)
                        {
                            puntoAnclaje = anclaje;
                            anclajeCercano = distancia;
                        }
                    }
                    else
                    {
                        if (anclaje.transform.position.x < transform.position.x)
                        {
                            puntoAnclaje = anclaje;
                            anclajeCercano = distancia;
                        }
                    }
                }
            }

            float distanciaPuntoAnclaje = 0;
            
            if (puntoAnclaje != null)
            {
                distanciaPuntoAnclaje = Vector2.Distance(transform.position, puntoAnclaje.transform.position);
                
                if (distanciaPuntoAnclaje <= 15)
                {
                    indicadorLanzarGancho.GetComponent<Renderer>().material.color = Color.green;
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
                    boxCollider.enabled = false;
                    direccionDisparargancho = new Vector2(puntoAnclaje.transform.position.x - transform.position.x,
                        puntoAnclaje.transform.position.y - transform.position.y);

                    RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direccionDisparargancho);

                    Debug.DrawRay(transform.position, direccionDisparargancho, Color.red);

                    if (hitInfo.collider.tag.Equals("PuntoAnclaje"))
                    {
                        puedeDisparar = true;
                    }
                    else
                    {
                        puedeDisparar = false;
                        indicadorLanzarGancho.GetComponent<Renderer>().material.color = Color.red;
                    }

                    boxCollider.enabled = true;
                }

                if (puedeDisparar)
                {
                    if (Input.GetButtonDown("Fire2"))
                    {
                        distanciaGanchoInicio = Vector2.Distance(transform.position, puntoAnclaje.transform.position);
                        if (!puntoAnclaje.GetComponent<PuntoAnclajeScript>().tipoEnganche.Equals("balanceo"))
                        {
                            playerMovement.mov = false;
                        }
                        ganchoDisparado = true;
                    }
                }
            }
        }

        if (ganchoDisparado)
        {
            float distancia = Vector3.Distance(transform.position, puntoAnclaje.transform.position);
            Debug.Log(distancia);
            if (distancia > 2f)
            {
                lanzarGanchoAnclaje();
            }
            else
            {
                if (transform.position.x > puntoAnclaje.transform.position.x)
                {
                    rigidbody.AddForce(Vector2.left * 5, ForceMode2D.Impulse);
                }
                else
                {
                    rigidbody.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
                }

                puntosAnclaje.Remove(puntoAnclaje);

                if (puntoAnclaje.GetComponent<PuntoAnclajeScript>().tipoEnganche.Equals("enemigo"))
                {
                    puntoAnclaje.SendMessage("hit", 1);
                    cameraController.shakeDuration = 1;
                }

                rigidbody.gravityScale = 1;
                playerMovement.mov = true;
                ganchoDisparado = false;
            }
        }
    }

    private IEnumerator putMovFalse()
    {
        yield return new WaitForSeconds(0.2f);
        rigidbody.gravityScale = 1;
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

            float distanciaGancho = Vector2.Distance(puntoAnclaje.transform.position, transform.position);

            velocidadGancho = (ganchoSpeed * distanciaGancho) / distanciaGanchoInicio;
        
            transform.position = Vector3.MoveTowards(transform.position, puntoAnclaje.transform.position, velocidadGancho);
        } else if (puntoAnclajeScript.tipoEnganche.Equals("balanceo"))
        {
            distanceJoint.enabled = true;
            distanceJoint.connectedBody = puntoAnclaje.GetComponent<Rigidbody2D>();

            if (Input.GetButtonDown("Jump"))
            {
                distanceJoint.enabled = false;
                rigidbody.velocity = rigidbody.velocity / 3;
                rigidbody.AddForce(Vector2.up * fuerzaSalidaGancho, ForceMode2D.Impulse);
                
                /*if (transform.position.x < puntoAnclaje.transform.position.x)
                {
                    rigidbody.AddForce((Vector2.left + Vector2.up) * fuerzaSalidaGancho, ForceMode2D.Impulse);
                }
                else
                {
                    rigidbody.AddForce((Vector2.right + Vector2.up) * fuerzaSalidaGancho, ForceMode2D.Impulse);
                }*/
                
                rigidbody.gravityScale = 1;
                playerMovement.mov = true;
                ganchoDisparado = false;
            }
        }
        
    }
}
