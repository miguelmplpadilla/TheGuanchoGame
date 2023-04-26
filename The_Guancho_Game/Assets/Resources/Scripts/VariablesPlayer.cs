using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NuevoJugador", menuName = "Jugador")]
public class VariablesPlayer : ScriptableObject
{
    public float vida = 3;
    public float kunais = 5;
    public int llaves = 0;

    private RectTransform imagenVida;
    private RectTransform imagenKunai;
    private RectTransform imagenLlave;

    public void inicializacion()
    {
        imagenVida = GameObject.Find("VidaUI").GetComponent<RectTransform>();
        imagenKunai = GameObject.Find("KunaiUI").GetComponent<RectTransform>();
        imagenLlave = GameObject.Find("LlaveUI").GetComponent<RectTransform>();
    }

    public void sumarVida(int vidaSumar)
    {
        if ((vida + vidaSumar) >= 3)
        {
            vida = 3;
        } else
        {
            vida += vidaSumar;
        }
    }
    
    public void restarVida(int vidaRestar)
    {
        vida -= vidaRestar;
    }
    
    public void sumarKunais(int kunaisSumar)
    {
        if ((kunais + kunaisSumar) >= 5)
        {
            kunais = 5;
        } else
        {
            kunais += kunaisSumar;
        }
    }
    
    public void restarLlaves(int num)
    {
        llaves -= num;
    }
    
    public void sumarLlaves(int num)
    {
        llaves += num;
    }
    
    public void restarKunais(int kunaisRestar)
    {
        kunais -= kunaisRestar;
    }
    
    public void actualizarVariables()
    {
        imagenVida.sizeDelta = new Vector2(222*vida, imagenVida.sizeDelta.y);
        imagenKunai.sizeDelta = new Vector2(200*kunais, imagenKunai.sizeDelta.y);
        
        if (llaves > 0)
        {
            imagenLlave.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            imagenLlave.localScale = new Vector3(0, 1, 1);
        }
    }

    public void reiniciarVariables()
    {
        vida = 3;
        kunais = 5;
        llaves = 0;
    }
}
