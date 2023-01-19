using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public bool isGrounded = true;
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Suelo"))
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
        }
    }
}
