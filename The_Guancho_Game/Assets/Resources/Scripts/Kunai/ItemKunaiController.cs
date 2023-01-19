using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKunaiController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerCombateController playerCombateController = col.GetComponent<PlayerCombateController>();
            if (playerCombateController.numKunais < 5)
            {
                playerCombateController.numKunais++;
                Destroy(gameObject);
            }
        }
    }
}
