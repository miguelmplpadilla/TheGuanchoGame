using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator3DTo2DController : MonoBehaviour
{
    public Animator animator;

    public float speedAnimacion;
    public float numFrameAnimacion;
    public float sumFotogramas = 0.1f;

    public string nombreAnimacion = "";

    private string anteriorAnimacion = "";

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

        if (!nombreAnimacion.Equals(anteriorAnimacion))
        {
            Debug.Log("Animacion diferente");
            numFrameAnimacion = 0;
            anteriorAnimacion = nombreAnimacion;
        }
    }

    IEnumerator animar()
    {
        while (true)
        {
            animator.Play(nombreAnimacion, 0, numFrameAnimacion);
            numFrameAnimacion += sumFotogramas;
            if (numFrameAnimacion > 1.1f)
            {
                numFrameAnimacion = 0;
            }
            yield return new WaitForSeconds(speedAnimacion);
        }
    }
}