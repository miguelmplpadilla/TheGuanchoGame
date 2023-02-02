using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombateController : MonoBehaviour
{
    public GameObject kunai;
    public GameObject shootPoint;

    public VariablesPlayer variablesPlayer;

    private Vector2 direccionLanzarKunai;

    public float speedKunai = 2;

    [SerializeField]
    private bool isKunaiLanzado = false;

    void Update()
    {
        if (!isKunaiLanzado && variablesPlayer.kunais > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject kunaiLanzado = Instantiate(kunai);
                kunaiLanzado.transform.position = shootPoint.transform.position;

                direccionLanzarKunai = new Vector2(transform.localScale.x, 0);
            
                kunaiLanzado.GetComponent<Rigidbody2D>().AddForce(direccionLanzarKunai * speedKunai, ForceMode2D.Impulse);

                StartCoroutine(contadorDelayLanzamientoKunai());

                variablesPlayer.restarKunais(1);

                isKunaiLanzado = true;
            }
        }
    }

    IEnumerator contadorDelayLanzamientoKunai()
    {
        yield return new WaitForSeconds(0.3f);
        isKunaiLanzado = false;
    }
}
