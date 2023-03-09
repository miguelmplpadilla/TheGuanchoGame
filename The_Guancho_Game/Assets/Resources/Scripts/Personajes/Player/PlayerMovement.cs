using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    public float speedMin;
    public float speedMax;
    
    public float jumpForce;
    
    public Vector2 movement;

    private Rigidbody2D rigidbody;

    private GroundController groundController;
    private CharacterController2D characterController2D;
    private PlayerGanchoController playerGanchoController;

    public bool mov = true;

    private float horizontalInput = 0;
    private float verticalInput = 0;

    public GameObject textoPlayer;

    public float maxVerticalSpeed = 100;
    public float minVerticalSpeed = -10;

    public float horizontalVelocity = 0;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        groundController = GetComponentInChildren<GroundController>();
        characterController2D = GetComponent<CharacterController2D>();
        playerGanchoController = GetComponent<PlayerGanchoController>();
    }

    void Update()
    {
        if (mov)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");

            if (!groundController.isGrounded)
            {
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    verticalInput = -1;
                }
                else
                {
                    verticalInput = 0;
                }
            }
            else
            {
                verticalInput = 0;
            }


            // Flip character
            if (horizontalInput > 0f) {
                transform.localScale = new Vector3(1, 1, 1);
                textoPlayer.transform.localScale = new Vector3(1,1,1);
            } else if (horizontalInput < 0f) {
                transform.localScale = new Vector3(-1, 1, 1);
                textoPlayer.transform.localScale = new Vector3(-1,1,1);
            }

            if (groundController.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    saltar();
                }
            }

            if (Input.GetButton("Sprint"))
            {
                speed = speedMax;
            }
            else
            {
                speed = speedMin;
            }
            
            movement = new Vector2(horizontalInput, verticalInput) * Time.deltaTime;
            horizontalVelocity = movement.normalized.x * speed;
        }
    }

    public void saltar()
    {
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        float y =  Mathf.Clamp(rigidbody.velocity.y, minVerticalSpeed, maxVerticalSpeed);

        if (mov)
        {
            //rigidbody.velocity = new Vector2(horizontalVelocity, y);

            if (playerGanchoController.ganchoEnganchado && playerGanchoController.tipoEnganche.Equals("balanceo"))
            {
                transform.Translate(movement * speed);
            }
            else
            {
                rigidbody.velocity =
                    transform.TransformDirection(new Vector3(horizontalVelocity, rigidbody.velocity.y, 0));
            }
        }
    }
}
