using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class KunaiController : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private GameObject particulaRayo;

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
        Debug.Log("Colision con: "+col.name);
        
        if (col.CompareTag("HurtBoxEnemigo"))
        {
            player.GetComponent<PlayerGanchoController>().puntosAnclaje.Remove(col.transform.parent.gameObject);
            col.SendMessage("hit", 10);
        }

        if (!col.CompareTag("Player") &&
            (col.CompareTag("Untagged") || col.CompareTag("Suelo") || col.CompareTag("HurtBoxEnemigo")))
        {
            if (col.CompareTag("Suelo"))
            {
                GameObject rayo = Instantiate(particulaRayo, transform.position, Quaternion.identity);

                if (transform.rotation.y == 0)
                {
                    rayo.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1,0,0);
                    rayo.transform.rotation = quaternion.Euler(0, 180, 0);
                }
            }

            Destroy(gameObject);
        }
    }
}