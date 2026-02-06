using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public float velocidad = 2;
    public GameObject col;      // El suelo
    public GameObject seta;     // Obstáculo 1
    public GameObject tulipan;  // Obstáculo 2
    public Renderer fondo;

    // Listas para guardar lo que creamos
    public List<GameObject> cols = new List<GameObject>();
    public List<GameObject> obstaculos = new List<GameObject>(); // NUEVA LISTA

    void Start()
    {
        for (int i = 0; i < 21; i++)
        {
            cols.Add(Instantiate(col, new Vector2(-10 + i, -3), Quaternion.identity));
        }

        for (int i = 0; i < 6; i++)
        {
            GameObject prefabElegido = (Random.value > 0.5f) ? seta : tulipan;

            float spawnX = 10 + (i * 7); // Separados por 7 metros cada uno
            
            float spawnY = -2.2f; 

            // Lo creamos y lo guardamos en la lista
            obstaculos.Add(Instantiate(prefabElegido, new Vector2(spawnX, spawnY), Quaternion.identity));
        }
    }




    void Update()
    {
        fondo.material.mainTextureOffset += new Vector2(0.1f, 0) * Time.deltaTime;

        for (int i = 0; i < cols.Count; i++)
        {
            cols[i].transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;

            if (cols[i].transform.position.x < -10)
            {
                float nuevaX = cols[i].transform.position.x + 21f;
                cols[i].transform.position = new Vector3(nuevaX, -3, 0);
            }
        }

        for (int i = 0; i < obstaculos.Count; i++)
        {
            obstaculos[i].transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;

            if (obstaculos[i].transform.position.x < -10)
            {
                float nuevaX = Random.Range(30f, 45f);
                
                obstaculos[i].transform.position = new Vector3(nuevaX, -2.2f, 0);
            }
        }
    }
}