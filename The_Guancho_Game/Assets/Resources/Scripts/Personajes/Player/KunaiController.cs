using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiController : MonoBehaviour
{
    
    private GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine("destruirKunai");
    }

    IEnumerator destruirKunai()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PuntoAnclaje"))
        {
            if (col.GetComponent<PuntoAnclajeScript>().tipoEnganche.Equals("enemigo"))
            {
                player.GetComponent<PlayerGanchoController>().puntosAnclaje.Remove(col.gameObject);
                col.SendMessage("hit", 1);
            }
        }

        if (!col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
