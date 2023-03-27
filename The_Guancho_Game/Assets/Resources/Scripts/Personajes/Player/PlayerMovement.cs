using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    [SerializeField] private float speedMin;
    [SerializeField] private float speedMax;
    [SerializeField] private float speedAir;

    [SerializeField] private float sumSpeed = 1;
    [SerializeField] private float restSpeed = 1;
    
    public float jumpForce;
    
    public Vector2 movement;

    private Rigidbody2D rigidbody;

    private GroundController groundController;
    private PlayerGanchoController playerGanchoController;

    public bool mov = true;

    private float horizontalInput = 0;
    private float verticalInput = 0;

    public GameObject textoPlayer;

    public float maxVerticalSpeed = 100;
    public float minVerticalSpeed = -10;

    public float horizontalVelocity = 0;

    public Animator animator;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        groundController = GetComponentInChildren<GroundController>();
        playerGanchoController = GetComponent<PlayerGanchoController>();
    }

    void Update()
    {
        if (mov)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");

            if (horizontalInput != 0)
            {
                if (groundController.isGrounded)
                {
                    if (Input.GetButton("Sprint"))
                    {
                        speed = speedMax;
                    }
                    else
                    {
                        speed = speedMin;
                    }
                }
                else
                {
                    speed = speedAir;
                }

                animator.SetBool("run", true);
            }
            else
            {
                speed = 0;
                
                animator.SetBool("run", false);
            }

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


            if (!playerGanchoController.ganchoEnganchado)
            {
                if (horizontalInput > 0f) {
                    transform.localScale = new Vector3(1, 1, 1);
                    textoPlayer.transform.localScale = new Vector3(1,1,1);
                } else if (horizontalInput < 0f) {
                    transform.localScale = new Vector3(-1, 1, 1);
                    textoPlayer.transform.localScale = new Vector3(-1,1,1);
                }
            }

            if (groundController.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    saltar();
                }
            }

            movement = new Vector2(horizontalInput, verticalInput) * Time.deltaTime;
            horizontalVelocity = movement.normalized.x * speed;
        }
    }

    public void saltar()
    {
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        //animator.SetTrigger("jump");
    }

    private void LateUpdate()
    {
        animator.SetBool("isGrounded", groundController.isGrounded);
    }

    private void FixedUpdate()
    {
        animator.SetFloat("horizontalVelocity", speed);
        
        float y =  Mathf.Clamp(rigidbody.velocity.y, minVerticalSpeed, maxVerticalSpeed);

        if (mov)
        {
            if (playerGanchoController.ganchoEnganchado && playerGanchoController.tipoEnganche.Equals("balanceo"))
            {
                transform.Translate(movement * speed);
            }
            else
            {
                rigidbody.velocity =
                    transform.TransformDirection(new Vector3(horizontalVelocity, y, 0));
            }
        }
    }
}
