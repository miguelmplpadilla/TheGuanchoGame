using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    
    private Vector2 movement;

    private Rigidbody2D rigidbody;

    private GroundController groundController;

    public bool mov = true;

    private float horizontalInput = 0;
    private float verticalInput = 0;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        groundController = GetComponentInChildren<GroundController>();
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

            movement = new Vector2(horizontalInput, verticalInput) * Time.deltaTime;
            

            // Flip character
            if (horizontalInput > 0f) {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            } else if (horizontalInput < 0f) {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }

            if (groundController.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    saltar();
                }
            }
            
        }
    }
    
    public void saltar()
    {
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void LateUpdate()
    {
        if (mov)
        {
            float horizontalVelocity = movement.normalized.x * speed;
            //rigidbody.velocity = new Vector2(horizontalVelocity, rigidbody.velocity.y);
            
            transform.Translate(movement * speed);
        }
    }
}
