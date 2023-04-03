using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator3DTo2DController : MonoBehaviour
{
    public Animator animator;

    public float speedAnimacion;
    public float numFrameAnimacion = 0.1f;
    public float sumFotogramas = 0.1f;

    public string nombreAnimacion = "";

    private string anteriorAnimacion = "";

    private bool isLooping;
    private bool sumarParado = false;

    public string funcionEjecutarFinal;
    public string animacionEjecutarFuncion;

    public string[] animacionesExcluidas;

    public bool animacionEsExcluida = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }

    private void Start()
    {
        StartCoroutine("animar");
    }

    private void Update()
    {
        nombreAnimacion = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        sumFotogramas = (animator.GetCurrentAnimatorClipInfo(0)[0].clip.length * 0.1f) / 0.8f;

        isLooping = animator.GetCurrentAnimatorClipInfo(0)[0].clip.isLooping;

        if (!nombreAnimacion.Equals(anteriorAnimacion))
        {
            sumarParado = false;
            numFrameAnimacion = 0.1f;
            anteriorAnimacion = nombreAnimacion;
        }

        animacionEsExcluida = animacionExcluida();
    }

    IEnumerator animar()
    {
        while (true)
        {
            if (!animacionEsExcluida)
            {
                if (!sumarParado)
                {
                    animator.speed = 0;
                    animator.Play(nombreAnimacion, 0, numFrameAnimacion);
                    numFrameAnimacion += sumFotogramas;
                    if (numFrameAnimacion > 0.9f)
                    {
                        if (isLooping)
                        {
                            numFrameAnimacion = 0.1f;
                        }
                        else
                        {
                            if (!animacionEjecutarFuncion.Equals("") && animacionEjecutarFuncion.Equals(nombreAnimacion))
                            {
                                gameObject.SendMessage(funcionEjecutarFinal);
                            }

                            numFrameAnimacion = 1;
                        
                            animator.Play(nombreAnimacion, 0, numFrameAnimacion);
                        
                            sumarParado = true;
                        }
                    }
                
                    yield return new WaitForSeconds(speedAnimacion);
                }
            }
            else
            {
                numFrameAnimacion = 0;
                animator.speed = 1;
            }
            
            yield return null;
        }
    }

    private bool animacionExcluida()
    {
        bool excluir = false;

        for (int i = 0; i < animacionesExcluidas.Length; i++)
        {
            if (animacionesExcluidas[i].Equals(nombreAnimacion))
            {
                excluir = true;
            }
        }

        return excluir;
    }
}