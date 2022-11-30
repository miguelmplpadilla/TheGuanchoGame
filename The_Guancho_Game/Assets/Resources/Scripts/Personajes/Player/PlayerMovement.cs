using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float ganchoSpeed = 1;

    public GameObject puntoAnclaje;
    
    private Vector2 movement;

    private Rigidbody2D rigidbody;

    private bool ganchoDisparado = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(horizontalInput, 0f);

        // Flip character
        if (horizontalInput > 0f) {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        } else if (horizontalInput < 0f) {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            saltar();
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            ganchoDisparado = true;
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
                ganchoDisparado = false;
            }
        }
    }
    
    public void saltar()
    {
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void lanzarGanchoAnclaje()
    {
        rigidbody.gravityScale = 0;
        transform.position = Vector3.MoveTowards(transform.position, puntoAnclaje.transform.position, ganchoSpeed);
    }

    private void LateUpdate()
    {
        float horizontalVelocity = movement.normalized.x * speed;
        rigidbody.velocity = new Vector2(horizontalVelocity, rigidbody.velocity.y);
    }
}
