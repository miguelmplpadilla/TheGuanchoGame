using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHurtController : MonoBehaviour
{
    [SerializeField] private VariablesPlayer variablesPlayer;
    private Animator animator;
    private PlayerMovement playerMovement;

    private Animator animatorPanelRojo;

    public bool muerto = false;

    private void Awake()
    {
        animator = transform.parent.GetComponentInChildren<Animator>();
        playerMovement = transform.parent.GetComponentInChildren<PlayerMovement>();
    }

    private void Start()
    {
        animatorPanelRojo = GameObject.Find("PanelRojo").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!muerto)
        {
            if (col.CompareTag("HitBoxEnemigo"))
            {
                if (variablesPlayer.vida > 0)
                {
                    animatorPanelRojo.SetTrigger("rojo");
                    variablesPlayer.restarVida(col.GetComponent<EnemigoDamageController>().damage);
                }
                
                if (variablesPlayer.vida <= 0)
                {
                    StartCoroutine("reiniciarEscena");
                    playerMovement.mov = false;
                    animator.SetTrigger("morir");
                    muerto = true;
                }
            }
        }
    }

    IEnumerator reiniciarEscena()
    {
        yield return new WaitForSeconds(2f);
        
        PlayerPrefs.SetString("EscenaAnterior", SceneManager.GetActiveScene().name);
        
        SceneManager.LoadScene("EscenaMuerte");
    }
}