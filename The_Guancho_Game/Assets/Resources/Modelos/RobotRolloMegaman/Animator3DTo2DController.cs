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
    }

    IEnumerator animar()
    {
        while (true)
        {
            if (!sumarParado)
            {
                animator.Play(nombreAnimacion, 0, numFrameAnimacion);
                numFrameAnimacion += sumFotogramas;
                if (numFrameAnimacion > 1f)
                {
                    if (isLooping)
                    {
                        numFrameAnimacion = 0.1f;
                    }
                    else
                    {
                        numFrameAnimacion = 1;
                        
                        animator.Play(nombreAnimacion, 0, numFrameAnimacion);
                        
                        sumarParado = true;
                    }
                }
                
                yield return new WaitForSeconds(speedAnimacion);
            }

            yield return null;
        }
    }
}