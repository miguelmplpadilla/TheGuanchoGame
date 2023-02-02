using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKunaiController : MonoBehaviour
{

    public VariablesPlayer variablesPlayer;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerCombateController playerCombateController = col.GetComponent<PlayerCombateController>();
            if (variablesPlayer.kunais < 5)
            {
                variablesPlayer.sumarKunais(1);
                Destroy(gameObject);
            }
        }
    }
}
