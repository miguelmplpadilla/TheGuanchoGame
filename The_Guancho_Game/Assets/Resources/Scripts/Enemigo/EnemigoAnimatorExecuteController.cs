using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoAnimatorExecuteController : MonoBehaviour
{
    private EnemigoIA enemigoIa;

    [SerializeField] private GameObject bala;
    [SerializeField] private GameObject puntoInstanciarBala;

    [SerializeField] private float velocidadBala = 2;

    public Vector3 direccionDisparar = new Vector3(0, 0, 0);


    private void Awake()
    {
        enemigoIa = transform.parent.GetComponentInParent<EnemigoIA>();
    }

    public void disparar()
    {
        GameObject balaInstanciada = Instantiate(bala, puntoInstanciarBala.transform.position, Quaternion.identity);

        Vector2 direccionDispararBala;

        if (direccionDisparar.x > transform.position.x)
        {
            direccionDispararBala = Vector2.right;
        }
        else
        {
            direccionDispararBala = Vector2.left;
        }

        balaInstanciada.GetComponent<Rigidbody2D>().AddForce(direccionDispararBala * velocidadBala,
            ForceMode2D.Impulse);
    }

    public void setAtacandoFalse()
    {
        enemigoIa.atacando = false;
    }
}