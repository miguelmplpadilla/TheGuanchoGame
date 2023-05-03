using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonInteractuarController : MonoBehaviour {

    public Sprite teclado;
    public Sprite mando;
    private SpriteRenderer spriteRenderer;

    private Vector3 originalLocalScale;

    [SerializeField] private bool mostrar;

    private void Awake() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalLocalScale = transform.localScale;

        if (!mostrar)
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
    }

    void Update()
    {
        if (Input.GetJoystickNames().Length > 0) {
            spriteRenderer.sprite = mando;
        }
        else {
            spriteRenderer.sprite = teclado;
        }
    }

    public void visible() {
        if (transform.localScale.x != 0) {
            transform.localScale = new Vector3(0, 0, 0);
            spriteRenderer.enabled = false;
        }
        else {
            transform.localScale = originalLocalScale;
            spriteRenderer.enabled = true;
        }
    }
}
