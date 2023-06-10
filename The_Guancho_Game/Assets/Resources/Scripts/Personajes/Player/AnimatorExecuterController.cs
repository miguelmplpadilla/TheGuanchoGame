using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorExecuterController : MonoBehaviour
{
    private PlayerCombateController playerCombateController;
    private PlayerController playerController;
    private Animator animator;

    [SerializeField] private GameObject particulasCorrer;
    [SerializeField] private GameObject puntoCreacionparticulasSuelo;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.parent.localScale;
        
        playerController = GetComponentInParent<PlayerController>();
        playerCombateController = GetComponentInParent<PlayerCombateController>();

        animator = GetComponent<Animator>();
    }

    public void throwKunai()
    {
        playerCombateController.lanzarKunai();

        StartCoroutine("stopThrowingKunai");
    }

    IEnumerator stopThrowingKunai()
    {
        yield return new WaitForSeconds(0.2f);

        playerCombateController.isKunaiLanzado = false;
        playerController.mov = true;
        animator.SetBool("throwing", false);
    }
    
    private void crearParticulasCorrer()
    {
        GameObject particulaInstanciada = Instantiate(particulasCorrer, puntoCreacionparticulasSuelo.transform.position, Quaternion.identity);
        
        if (transform.parent.localScale.x == -originalScale.x)
        {
            particulaInstanciada.transform.rotation = Quaternion.Euler(0,180,0);
            particulaInstanciada.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
        }
    }
}