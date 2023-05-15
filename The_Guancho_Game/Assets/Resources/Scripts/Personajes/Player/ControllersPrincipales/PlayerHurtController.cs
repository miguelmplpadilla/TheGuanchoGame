using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHurtController : MonoBehaviour
{
    [SerializeField] private VariablesPlayer variablesPlayer;
    private Animator animator;
    private PlayerController playerController;
    private PausaController pausaController;

    private Animator animatorPanelRojo;

    public bool muerto = false;

    private void Awake()
    {
        animator = transform.parent.GetComponentInChildren<Animator>();
        playerController = GetComponentInParent<PlayerController>();
    }

    private void Start()
    {
        pausaController = GameObject.Find("PausaManager").GetComponent<PausaController>();
        animatorPanelRojo = GameObject.Find("PanelRojo").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!muerto && !pausaController.pausado)
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
                    playerController.mov = false;
                    playerController.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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