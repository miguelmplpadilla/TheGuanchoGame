using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoAnimatorExecuteController : MonoBehaviour
{
    private EnemigoIA enemigoIa;

    private void Awake()
    {
        enemigoIa = transform.parent.GetComponentInParent<EnemigoIA>();
    }

    public void setAtacandoFalse()
    {
        enemigoIa.atacando = false;
    }
}
