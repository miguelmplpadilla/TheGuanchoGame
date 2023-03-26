using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator3DTo2DController : MonoBehaviour
{
    public Animator animator;

    public float speedAnimacion;
    public float numFrameAnimacion;

    public string nombreAnimacion = "";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine("animar");
    }

    private void Update()
    {
        animator.speed = 0;
    }

    IEnumerator animar()
    {
        while (true)
        {
            animator.Play(nombreAnimacion, 0, numFrameAnimacion);
            numFrameAnimacion += 0.1f;
            if (numFrameAnimacion > 1)
            {
                numFrameAnimacion = 0;
            }
            yield return new WaitForSeconds(speedAnimacion);
        }
    }
}