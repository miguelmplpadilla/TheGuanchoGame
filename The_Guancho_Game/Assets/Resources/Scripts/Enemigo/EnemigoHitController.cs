using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoHitController : MonoBehaviour
{
    [SerializeField]
    private float vida = 1;
    
    public void hit(float dano)
    {
        if (vida > 0)
        {
            vida -= dano;
        }

        if (vida <= 0)
        {
            // Aqui va la animacion de morir y despues se destruye
            destruirEnemigo();
        }
    }

    public void destruirEnemigo()
    {
        Destroy(gameObject);
    }
}
