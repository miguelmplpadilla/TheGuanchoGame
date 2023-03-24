using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCManager : MonoBehaviour
{
    public TMP_Text textComponent;

    public string textoEscribir;

    private List<List<Vector3>> vertexOrig = new List<List<Vector3>>();
    private List<List<Vector3>> tamanoVertices = new List<List<Vector3>>();

    //private Mesh mesh;
    private Vector3[] vertices;

    public Color32 color;
    private List<Color32> coloresActuales = new List<Color32>();

    [Serializable]
    public class TipoFrase
    {
        public int wordIndex;
        public string tipoFrase;

        public TipoFrase(int w, string t)
        {
            wordIndex = w;
            tipoFrase = t;
        }
    }

    [SerializeField] private List<TipoFrase> wordIndexes;
    [SerializeField] private List<int> wordLengths;

    private void Start()
    {
        textComponent.ForceMeshUpdate();

        textComponent.text = textoEscribir;
        
        initialiceWordIndexer();

        StartCoroutine("mostrarTextoV1");
    }

    private void Update()
    {
        //temblarVertices();
        //temblarCaracteres();
        //temblarPalabras();
        
        //moverCaracteresArribaAbajo();

        //moverTextoArribaAbajo();
    }

    private void LateUpdate()
    {
        moverCaracteresArribaAbajo();
    }

    private void initialiceWordIndexer()
    {
        wordIndexes = new List<TipoFrase> {new TipoFrase(0, "")};
        wordLengths = new List<int>();

        string s = textComponent.text;
        
        string[] values = Regex.Split(s, " ");

        for (int i = s.IndexOf(' '); i > -1; i = s.IndexOf(' ', i + 1))
        {
            wordLengths.Add(i - wordIndexes[wordIndexes.Count - 1].wordIndex);
            wordIndexes.Add(new TipoFrase(i+1, ""));
        }
        
        wordLengths.Add(s.Length - wordIndexes[wordIndexes.Count - 1].wordIndex);
        
        for (int i = 0; i < values.Length; i++)
        {
            string tresCaracteres = values[i].Substring(0, 3);
            
            if (tresCaracteres.Equals("<t>"))
            {
                wordIndexes[i].tipoFrase = "t";

                wordLengths[i] -= 3;
                
                for (int j = i+1; j < wordIndexes.Count; j++)
                {
                    wordIndexes[j].wordIndex -= 3;
                }
            }
        }

        textComponent.text = textComponent.text.Replace("<t>", "");
    }

    private void temblarVertices()
    {
        textComponent.ForceMeshUpdate();
        Mesh mesh = textComponent.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 offset = new Vector3(Random.Range(1.5f,5f), Random.Range(1.5f,5f), Random.Range(1.5f,5f));

            vertices[i] = vertices[i] + offset;
        }

        mesh.vertices = vertices;
        textComponent.canvasRenderer.SetMesh(mesh);
    }
    
    private void temblarCaracteres()
    {
        textComponent.ForceMeshUpdate();
        Mesh mesh = textComponent.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < textComponent.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textComponent.textInfo.characterInfo[i];

            int index = c.vertexIndex;
            
            Vector3 offset = new Vector3(Random.Range(1.5f,5f), Random.Range(1.5f,5f), Random.Range(1.5f,5f));

            vertices[index] += offset;
            vertices[index+1] += offset;
            vertices[index+2] += offset;
            vertices[index+3] += offset;
        }

        mesh.vertices = vertices;
        textComponent.canvasRenderer.SetMesh(mesh);
    }
    
    private void temblarPalabras()
    {
        textComponent.ForceMeshUpdate();
        Mesh mesh = textComponent.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < wordIndexes.Count; i++)
        {
            if (wordIndexes[i].tipoFrase.Equals("t"))
            {
                int wordIndex = wordIndexes[i].wordIndex;
                Vector3 offset = new Vector3(Random.Range(1.5f,5f), Random.Range(1.5f,5f), Random.Range(1.5f,5f));
            
                for (int j = 0; j < wordLengths[i]; j++)
                {
                    TMP_CharacterInfo c = textComponent.textInfo.characterInfo[wordIndex + j];

                    if (c.isVisible)
                    {
                        int index = c.vertexIndex;

                        vertices[index] += offset;
                        vertices[index+1] += offset;
                        vertices[index+2] += offset;
                        vertices[index+3] += offset;
                        
                        mesh.vertices = vertices;
                        textComponent.canvasRenderer.SetMesh(mesh);
                    }
                }
            }
        }
    }

    private void moverCaracteresArribaAbajo()
    {
        textComponent.ForceMeshUpdate();
        Mesh mesh = textComponent.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < textComponent.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textComponent.textInfo.characterInfo[i];

            int index = c.vertexIndex;
            
            Vector3 orig = vertices[index];
            
            Vector3 offset = new Vector3(0, Mathf.Sin(Time.time * 2 + orig.x * 0.01f) * 10, 0);

            if (c.isVisible)
            {
                for (int j = 0; j < 4; j++)
                {
                    vertices[index + j] += offset;
                }
            
                mesh.vertices = vertices;
                textComponent.canvasRenderer.SetMesh(mesh);
            }
        }
    }

    private void moverTextoArribaAbajo()
    {
        var textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; j++)
            {
                if (tamanoVertices[i][j] != new Vector3(0, 0, 0))
                {
                    var orig = verts[charInfo.vertexIndex + j];

                    verts[charInfo.vertexIndex + j] =
                        orig + new Vector3(0, Mathf.Sin(Time.time * 2 + orig.x * 0.01f) * 10, 0);
                }
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }

    private void esconderTexto()
    {
        textComponent.ForceMeshUpdate();

        Mesh mesh = textComponent.mesh;
        Color32[] colores = mesh.colors32;

        for (int i = 0; i < textComponent.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textComponent.textInfo.characterInfo[i];
            int index = c.vertexIndex;
            
            Color32 colorNuevo = new Color32(0,0,0, 0);

            for (int j = 0; j < 4; j++)
            {
                colores[index + (j+1)] = colorNuevo;
                coloresActuales.Add(colorNuevo);
            }
        }
        
        mesh.colors32 = colores;
        textComponent.canvasRenderer.SetMesh(mesh);
    }

    IEnumerator mostrarTextoV2()
    {
        textComponent.ForceMeshUpdate();
        
        esconderTexto();

        Mesh mesh = textComponent.mesh;
        Color32[] colores = mesh.colors32;

        for (int i = 0; i < textComponent.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textComponent.textInfo.characterInfo[i];
            int index = c.vertexIndex;
            
            Color32 colorNuevo = new Color32(255,255,255, 255);
            
            for (int j = 0; j < 4; j++)
            {
                colores[index + (j+1)] = colorNuevo;
                coloresActuales[index + (j+1)] = colorNuevo;
            }

            yield return new WaitForSeconds(0.05f);
            
            mesh.colors32 = colores;
            textComponent.canvasRenderer.SetMesh(mesh);
        }

        yield return null;
    }

    IEnumerator mostrarTextoV1()
    {
        int totalVisibleCharacters = textoEscribir.Length;

        int cont = 0;

        while (true)
        {
            int visibleCount = cont % (totalVisibleCharacters + 1);

            textComponent.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                break;
            }

            cont++;

            yield return new WaitForSeconds(0.05f);
        }
    }
}