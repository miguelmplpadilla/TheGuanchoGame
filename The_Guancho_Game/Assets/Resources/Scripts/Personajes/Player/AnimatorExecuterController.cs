using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorExecuterController : MonoBehaviour
{
    private PlayerCombateController playerCombateController;
    private PlayerMovement playerMovement;
    private Animator animator;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
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
        playerMovement.mov = true;
        animator.SetBool("throwing", false);
    }
}