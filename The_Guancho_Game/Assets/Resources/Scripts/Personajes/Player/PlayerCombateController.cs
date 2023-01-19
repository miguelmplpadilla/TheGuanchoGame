using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombateController : MonoBehaviour
{
    public GameObject kunai;
    public GameObject shootPoint;

    private Vector2 direccionLanzarKunai;

    public float speedKunai = 2;

    [SerializeField]
    private bool isKunaiLanzado = false;

    public int numKunais = 5;

    private RectTransform kunaiUI;

    private void Start()
    {
        kunaiUI = GameObject.Find("KunaiUI").GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!isKunaiLanzado && numKunais > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject kunaiLanzado = Instantiate(kunai);
                kunaiLanzado.transform.position = shootPoint.transform.position;

                direccionLanzarKunai = new Vector2(transform.localScale.x, 0);
            
                kunaiLanzado.GetComponent<Rigidbody2D>().AddForce(direccionLanzarKunai * speedKunai, ForceMode2D.Impulse);

                StartCoroutine(contadorDelayLanzamientoKunai());

                numKunais--;

                isKunaiLanzado = true;
            }
        }

        kunaiUI.sizeDelta = new Vector2(200 * numKunais, kunaiUI.sizeDelta.y);
    }

    IEnumerator contadorDelayLanzamientoKunai()
    {
        yield return new WaitForSeconds(0.3f);
        isKunaiLanzado = false;
    }
}
