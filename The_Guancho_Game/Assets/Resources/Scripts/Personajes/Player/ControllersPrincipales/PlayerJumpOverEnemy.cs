using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpOverEnemy : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("HurtBoxEnemigo"))
        {
            float distancia = Vector2.Distance(transform.position, other.transform.position);
            
            if (distancia > 0.15f && transform.position.y > other.transform.position.y)
            {
                EnemigoHitController enemigoHitController = other.GetComponent<EnemigoHitController>();
                enemigoHitController.hit(enemigoHitController.vida);
                playerController.crearParticulaAterrizar();
                playerController.jump();
            }
        }
    }
}
