using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    public VariablesPlayer variablesPlayer;
    public string lootType;
    public int numLoot;

    private bool puedeRecoger = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Suelo"))
        {
            puedeRecoger = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (puedeRecoger)
        {
            if (other.CompareTag("Player"))
            {
                if (lootType.Equals("kunai"))
                {
                    variablesPlayer.sumarKunais(numLoot);
                }
                else if (lootType.Equals("vida"))
                {
                    variablesPlayer.sumarVida(numLoot);
                }

                Destroy(transform.parent.gameObject);
            }
        }
    }
}