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

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        groundController = GetComponentInChildren<GroundController>();
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

        if (groundController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                saltar();
            }
        }
    }
    
    public void saltar()
    {
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void LateUpdate()
    {
        float horizontalVelocity = movement.normalized.x * speed;
        rigidbody.velocity = new Vector2(horizontalVelocity, rigidbody.velocity.y);
    }
}
