using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public bool isGrounded = true;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Suelo"))
        {
            rigidbody.velocity = Vector2.zero;
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
