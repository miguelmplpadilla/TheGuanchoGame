using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    public bool isUp;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") < 0 && player.GetComponentInChildren<GroundController>().isGrounded)
        {
            transform.parent.GetComponent<Collider2D>().enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            transform.parent.GetComponent<Collider2D>().enabled = isUp;
        }
    }
}
