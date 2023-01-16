using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGanchoController : MonoBehaviour
{
    
    public float ganchoSpeed = 1;
    private float ganchoSpeedInicio = 1;
    public float ganchoFuerza = 2;

    private GameObject puntoAnclaje;
    private Vector3 posicionEnganche;
    private GameObject indicadorLanzarGancho;
    private PlayerMovement playerMovement;
    
    private bool ganchoDisparado = false;
    private bool puedeDisparar = true;
    
    private Rigidbody2D rigidbody;

    private BoxCollider2D boxCollider;

    private GameObject[] puntosAnclaje;

    private Vector2 direccionDisparargancho;

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
        puntosAnclaje = GameObject.FindGameObjectsWithTag("PuntoAnclaje");
    }

    void Update()
    {
        if (!ganchoDisparado)
        {
            float anclajeCercano = 100;
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

            float distanciaPuntoAnclaje = Vector2.Distance(transform.position, puntoAnclaje.transform.position);

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
                if (Input.GetButtonDown("Fire1"))
                {
                    playerMovement.mov = false;
                    ganchoDisparado = true;
                }
            }
        }
        
        if (ganchoDisparado)
        {
            float distancia = Vector3.Distance(transform.position, posicionEnganche);
            if (distancia > 0.1f)
            {
                lanzarGanchoAnclaje();
            }
            else
            {
                rigidbody.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
                //StartCoroutine("putMovFalse");
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
        rigidbody.gravityScale = 0;
        rigidbody.velocity = Vector2.zero;

        posicionEnganche = new Vector3(puntoAnclaje.transform.position.x, puntoAnclaje.transform.position.y - 1,
            puntoAnclaje.transform.position.z);
        
        transform.position = Vector3.MoveTowards(transform.position, posicionEnganche, ganchoSpeed);
    }
}
