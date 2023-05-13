using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaController : MonoBehaviour
{

    [SerializeField] private GameObject particulasDestruir;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        Invoke("destroySelf", 5);
    }

    private void destroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (meshRenderer.enabled)
        {
            if ((other.CompareTag("Player") && other.name.Equals("Player")) || other.CompareTag("Suelo"))
            {
                meshRenderer.enabled = false;
                Instantiate(particulasDestruir, transform.position, Quaternion.identity);
            }
        }
    }
}
