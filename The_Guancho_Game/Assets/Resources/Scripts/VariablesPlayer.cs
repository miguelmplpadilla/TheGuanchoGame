using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NuevoJugador", menuName = "Jugador")]
public class VariablesPlayer : ScriptableObject
{
    public float vida = 3;
    public float kunais = 5;

    private RectTransform imagenVida;
    private RectTransform imagenKunai;

    public void inicializacion()
    {
        imagenVida = GameObject.Find("VidaUI").GetComponent<RectTransform>();
        imagenKunai = GameObject.Find("KunaiUI").GetComponent<RectTransform>();
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
    
    public void restarKunais(int kunaisRestar)
    {
        kunais -= kunaisRestar;
    }

    public void actualizarVida()
    {
        imagenVida.sizeDelta = new Vector2(222*vida, imagenVida.sizeDelta.y);
    }
    
    public void actualizarKunais()
    {
        imagenKunai.sizeDelta = new Vector2(200*kunais, imagenKunai.sizeDelta.y);
    }

    public void reiniciarVariables()
    {
        vida = 3;
        kunais = 5;
    }
}
