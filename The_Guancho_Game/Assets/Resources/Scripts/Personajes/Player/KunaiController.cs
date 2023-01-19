using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiController : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine("destruirKunai");
    }

    IEnumerator destruirKunai()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemigo"))
        {
            col.SendMessage("hit", 1);
        }

        if (!col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
