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
    private GameObject indicadorLanzarGancho;
    private PlayerMovement playerMovement;
    
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
            float distancia = Vector3.Distance(transform.position, puntoAnclaje.transform.position);
            if (distancia > 2)
            {
                lanzarGanchoAnclaje();
            }
            else
            {
                rigidbody.gravityScale = 1;
                
                /*Vector2 dir = puntoAnclaje.transform.position - transform.position;
                dir = dir.normalized;

                Debug.Log(dir);
                
                //rigidbody.AddRelativeForce(dir * 10, ForceMode2D.Impulse);
                
                float Angle = 45;   // left hand rule!
 
                Quaternion rotation = Quaternion.Euler( 0, 0, Angle);   // make the rotation around the Z axis (going into the screen)
                
                rigidbody.AddForce(rotation * dir * 10, ForceMode2D.Impulse);*/
                
                //rigidbody.AddForce(Vector2.up * ganchoFuerza, ForceMode2D.Impulse);
                rigidbody.AddForce(new Vector3(30, 30, 0) * ganchoFuerza, ForceMode2D.Impulse);
                
                playerMovement.mov = true;
                ganchoDisparado = false;
            }
        }
    }

    private void LateUpdate()
    {
        
    }

    public void lanzarGanchoAnclaje()
    {
        rigidbody.gravityScale = 0;
        Debug.Log(rigidbody.velocity);
        transform.position = Vector3.MoveTowards(transform.position, puntoAnclaje.transform.position, ganchoSpeed);
    }
}
