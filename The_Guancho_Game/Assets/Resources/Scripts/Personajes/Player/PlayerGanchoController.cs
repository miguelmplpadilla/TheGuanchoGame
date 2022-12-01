using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGanchoController : MonoBehaviour
{
    
    public float ganchoSpeed = 1;
    private float ganchoSpeedInicio = 1;

    private GameObject puntoAnclaje;
    private GameObject indicadorLanzarGancho;
    
    private bool ganchoDisparado = false;
    private bool puedeDisparar = true;
    
    private Rigidbody2D rigidbody;

    private CapsuleCollider2D boxCollider;

    private GameObject[] puntosAnclaje;

    private Vector2 direccionDisparargancho;

    private void Awake()
    {
        ganchoSpeedInicio = ganchoSpeed;
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<CapsuleCollider2D>();
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
                Vector2 direccionDisparargancho = new Vector2(puntoAnclaje.transform.position.x - transform.position.x,
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
                    ganchoDisparado = true;
                }
            }
        }

        
    }

    private void LateUpdate()
    {
        if (ganchoDisparado)
        {
            float distancia = Vector3.Distance(transform.position, puntoAnclaje.transform.position);
            if (distancia > 2)
            {
                lanzarGanchoAnclaje();
            }
            else
            {
                rigidbody.gravityScale = 1;
                ganchoDisparado = false;
            }
        }
    }

    public void lanzarGanchoAnclaje()
    {
        rigidbody.gravityScale = 0;
        transform.position = Vector3.MoveTowards(transform.position, puntoAnclaje.transform.position, ganchoSpeed);
    }
}
