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
    private Animator animator;
    private PlayerController playerController;
    private Rigidbody2D rigidbody;
    private PlayerGanchoController playerGanchoController;
    private PlayerHurtController playerHurtController;

    private Vector2 direccionLanzarKunai;

    public float speedKunai = 2;

    public bool isKunaiLanzado = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody2D>();
        playerGanchoController = GetComponent<PlayerGanchoController>();
        
        playerHurtController = GetComponentInChildren<PlayerHurtController>();
    }

    void Update()
    {
        if (!isKunaiLanzado && variablesPlayer.kunais > 0 && playerController.isGrounded && !playerGanchoController.ganchoDisparado && !playerHurtController.muerto)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                rigidbody.velocity = Vector2.zero;
                animator.SetBool("throwing", true);
                playerController.mov = false;
                animator.SetTrigger("throw");
                    
                isKunaiLanzado = true;
            }
        }
    }

    public void lanzarKunai()
    {
        GameObject kunaiLanzado = Instantiate(kunai);
        kunaiLanzado.transform.position = shootPoint.transform.position;

        if (transform.localScale.x < 0)
        {
            kunaiLanzado.transform.rotation = Quaternion.Euler(0, 180, 90);
        }

        direccionLanzarKunai = new Vector2(transform.localScale.x, 0);
            
        kunaiLanzado.GetComponent<Rigidbody2D>().AddForce(direccionLanzarKunai * speedKunai, ForceMode2D.Impulse);
        
        StartCoroutine(contadorDelayLanzamientoKunai());

        variablesPlayer.restarKunais(1);
    }

    IEnumerator contadorDelayLanzamientoKunai()
    {
        yield return new WaitForSeconds(0.3f);
        isKunaiLanzado = false;
    }
}
