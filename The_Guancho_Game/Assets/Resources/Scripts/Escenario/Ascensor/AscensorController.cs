using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscensorController : MonoBehaviour
{
    private GameObject player;

    private GameObject puntoMover;

    [SerializeField] private GameObject punto1;
    [SerializeField] private GameObject punto2;

    private bool moverAscensor = false;

    [SerializeField] private float speedAscensor = 5;
    
    private BotonInteractuarController botonInteractuarController;

    [SerializeField] private GameObject puntoMoverPlayer;

    private void Awake()
    {
        botonInteractuarController = gameObject.GetComponentInChildren<BotonInteractuarController>();
    }

    private void Start()
    {
        puntoMover = punto2;
    }

    public void inter(GameObject p)
    {
        botonInteractuarController.visible();
        
        player = p;

        player.transform.parent.position = puntoMoverPlayer.transform.position;
        player.GetComponentInParent<PlayerController>().mov = false;

        moverAscensor = true;
    }
    
    public void interEnter(GameObject p)
    {
        botonInteractuarController.visible();
        p.transform.parent.parent = transform;
    }

    public void interExit(GameObject p)
    {
        botonInteractuarController.visible();
        p.transform.parent.parent = null;
    }

    private void Update()
    {
        if (moverAscensor)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, puntoMover.transform.position, speedAscensor * Time.deltaTime);

            float distanciaPunto = Vector3.Distance(transform.position, puntoMover.transform.position);

            if (distanciaPunto < 0.1f)
            {
                botonInteractuarController.visible();

                if (puntoMover.Equals(punto2))
                {
                    puntoMover = punto1;
                }
                else
                {
                    puntoMover = punto2;
                }

                player.GetComponentInParent<PlayerController>().mov = true;
                player.GetComponent<InteractuarController>().interactuando = false;
                
                moverAscensor = false;
            }
        }
    }
}
