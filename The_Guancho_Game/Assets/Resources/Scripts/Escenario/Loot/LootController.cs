using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    public VariablesPlayer variablesPlayer;
    public string lootType;
    public int numLoot;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (lootType.Equals("kunai"))
            {
                variablesPlayer.sumarKunais(numLoot);
            } else if (lootType.Equals("vida"))
            {
                variablesPlayer.sumarVida(numLoot);
            }
            
            Destroy(transform.parent.gameObject);
        }
    }
}
