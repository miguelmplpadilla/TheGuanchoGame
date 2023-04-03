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

    private void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("Colisionado: "+col.name);
        
        if (col.CompareTag("HurtBoxEnemigo"))
        {
            player.GetComponent<PlayerGanchoController>().puntosAnclaje.Remove(col.transform.parent.gameObject);
            col.SendMessage("hit", 1);
        }

        if (!col.CompareTag("Player") && (col.CompareTag("Untagged") || col.CompareTag("Suelo") || col.CompareTag("HurtBoxEnemigo")))
        {
            Destroy(gameObject);
        }
    }
}
