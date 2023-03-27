using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public bool isGrounded = true;
    private Rigidbody2D rigidbody;

    private Animator animator;

    private void Awake()
    {
        rigidbody = GetComponentInParent<Rigidbody2D>();

        animator = transform.parent.GetComponentInChildren<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Suelo"))
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Suelo"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Suelo"))
        {
            isGrounded = false;
            animator.SetTrigger("jump");
        }
    }
}
