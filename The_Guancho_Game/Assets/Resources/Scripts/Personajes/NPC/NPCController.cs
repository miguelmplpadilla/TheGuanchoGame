using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public TextAsset dialogos;

    private String currentFrase = "";

    private GameObject panelPlayer;
    public GameObject panelNPC;
    private TextMeshProUGUI textoPlayer;
    public TextMeshProUGUI textoNPC;

    private GameObject player;
    //private BotonInteractuarController botonInteractuarController;

    private List<Frase> frases = new List<Frase>();
    private DialogeController dialogeController;
    public string hablante = "NPC1";

    private bool hablar = false;
    private bool hablando = false;

    private InteractuarController interController;

    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake()
    {
        dialogeController = GetComponent<DialogeController>();
        //botonInteractuarController = gameObject.GetComponentInChildren<BotonInteractuarController>();
    }

    void Start()
    {
        cinemachineVirtualCamera = GameObject.Find("CM").GetComponent<CinemachineVirtualCamera>();
        
        panelPlayer = GameObject.Find("DialogoPlayer");
        textoPlayer = GameObject.Find("TextoPlayer").GetComponent<TextMeshProUGUI>();

        frases = dialogeController.getDialogos(dialogos, hablante);
        player = GameObject.Find("Player");
    }


    void Update()
    {
        /*if (hablar == true && hablando == false)
        {
            if (Input.GetButtonDown("Interactuar"))
            {
                hablando = true;
                panel.SetActive(true);
                player.GetComponent<PlayerController>().mov = false;
                StartCoroutine("mostrarFrase");
            }
        }

        if (hablando == true)
        {
            
        }*/
    }

    public void inter(GameObject intController)
    {
        if (hablando == false)
        {
            interController = intController.GetComponent<InteractuarController>();
            //botonInteractuarController.visible();
            hablando = true;
            player.GetComponent<PlayerMovement>().mov = false;
            StartCoroutine("mostrarFrase");
        }
    }

    public void interEnter()
    {
        //botonInteractuarController.visible();
    }

    public void interExit()
    {
        //botonInteractuarController.visible();
    }

    IEnumerator mostrarFrase()
    {
        bool seguir = true;

        TextMeshProUGUI textoHablante;

        cinemachineVirtualCamera.m_Lens.OrthographicSize = 0.5f;

        for (int i = 0; i < frases.Count; i++)
        {
            if (seguir)
            {
                for (int j = 0; j < frases[i].frase.Length; j++)
                {
                    if (frases[i].hablante == "Player")
                    {
                        cinemachineVirtualCamera.Follow = player.transform;
                        
                        textoHablante = textoPlayer;
                        panelPlayer.transform.localScale = new Vector3(1,1,1);
                        panelNPC.transform.localScale = new Vector3(0,0,0);
                    }
                    else
                    {
                        cinemachineVirtualCamera.Follow = transform;
                        
                        textoHablante = textoNPC;
                        panelPlayer.transform.localScale = new Vector3(0,0,0);
                        panelNPC.transform.localScale = new Vector3(1,1,1);
                    }

                    currentFrase = currentFrase + frases[i].frase[j];
                    textoHablante.text = currentFrase;
                    if (hablando == false)
                    {
                        currentFrase = "";
                        yield break;
                    }

                    yield return new WaitForSeconds(0.01f);
                }

                seguir = false;
                currentFrase = "";
            }

            while (!seguir)
            {
                if (Input.GetButtonDown("Interactuar"))
                {
                    seguir = true;
                }

                yield return null;
            }
        }
        
        cinemachineVirtualCamera.m_Lens.OrthographicSize = 1.1f;
        panelPlayer.transform.localScale = new Vector3(0,0,0);
        panelNPC.transform.localScale = new Vector3(0,0,0);

        //botonInteractuarController.visible();
        dejarHablar();
    }

    private void dejarHablar()
    {
        hablar = false;
        hablando = false;
        player.GetComponent<PlayerMovement>().mov = true;
        interController.GetComponent<InteractuarController>().interactuando = false;
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //botonInteractuarController.visible();
            //hablar = true;
            player = other.gameObject;
        }
    }*/

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //botonInteractuarController.visible();
            dejarHablar();
        }
    }
}