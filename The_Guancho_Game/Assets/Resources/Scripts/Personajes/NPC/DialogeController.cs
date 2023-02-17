using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogeController : MonoBehaviour {

    public string idioma = "ES";
    
    public List<Frase> getDialogos(TextAsset dialogos, string hablante)
    {
        List<Frase> textoDialogo = new List<Frase>();
        string fs = dialogos.text;
        string[] fLines = Regex.Split ( fs, "\n|\r|\r\n" );

        for (int i = 0; i < fLines.Length; i++)
        {
            string valueLine = fLines[i];
            string[] values = Regex.Split(valueLine, ",");
            
            if (values[0].Equals(hablante))
            {
                textoDialogo.Add(new Frase());
                
                textoDialogo[textoDialogo.Count-1].npcHablante = values[0].Replace(".", ",");
                
                textoDialogo[textoDialogo.Count-1].hablante = values[1].Replace(".", ",");
                
                if (idioma.Equals("ES"))
                {
                    textoDialogo[textoDialogo.Count-1].frase = values[2].Replace(".", ",");
                } else if (idioma.Equals("EN")) {
                    textoDialogo[textoDialogo.Count-1].frase = values[3].Replace(".", ",");
                }
            }
        }
        
        return textoDialogo;
    }
}
