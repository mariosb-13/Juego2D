using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Configuración del Mundo")]
    public float velocidadBase = 5f; 
    private float velocidadActual = 0f; 
    public float retrasoInicio = 1.2f;  // Ajusta al tiempo de la animación de pantalones
    public Renderer fondo;
    
    [Header("Prefabs y Listas")]
    public GameObject col;       
    public GameObject seta;      
    public GameObject tulipan;   
    public List<GameObject> cols = new List<GameObject>();
    public List<GameObject> obstaculos = new List<GameObject>();

    [Header("Interfaz de Usuario")]
    public TextMeshProUGUI textoPuntos; 
    private float puntuacion = 0;

    void Start()
    {
        // Crear suelo (30 bloques para evitar huecos visuales)
        for (int i = 0; i < 30; i++)
        {
            cols.Add(Instantiate(col, new Vector2(-15 + i, -3), Quaternion.identity));
        }

        // Crear obstáculos iniciales con separación segura
        for (int i = 0; i < 6; i++)
        {
            GameObject prefabElegido = (Random.value > 0.5f) ? seta : tulipan;
            float spawnX = 20 + (i * 12); 
            float spawnY = -2.2f; 
            obstaculos.Add(Instantiate(prefabElegido, new Vector2(spawnX, spawnY), Quaternion.identity));
        }

        // Esperar a que termine la animación inicial para arrancar
        Invoke("EmpezarMundo", retrasoInicio);
    }

    void EmpezarMundo() { velocidadActual = velocidadBase; }

    void Update()
    {
        if (velocidadActual > 0)
        {
            puntuacion += Time.deltaTime * 10;
            if (textoPuntos != null)
                textoPuntos.text = "SCORE: " + Mathf.FloorToInt(puntuacion).ToString();
        }

        // Movimiento del fondo (Parallax)
        fondo.material.mainTextureOffset += new Vector2(0.05f, 0) * Time.deltaTime * velocidadActual;

        // Movimiento y reciclaje del suelo
        ActualizarSuelo();

        // Movimiento y reciclaje de obstáculos
        ActualizarObstaculos();
    }

    void ActualizarSuelo()
    {
        for (int i = 0; i < cols.Count; i++)
        {
            cols[i].transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidadActual;
            if (cols[i].transform.position.x < -15)
            {
                float nuevaX = cols[i].transform.position.x + 30f;
                cols[i].transform.position = new Vector3(nuevaX, -3, 0);
            }
        }
    }

    void ActualizarObstaculos()
    {
        for (int i = 0; i < obstaculos.Count; i++)
        {
            obstaculos[i].transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidadActual;

            if (obstaculos[i].transform.position.x < -15)
            {
                // Al reciclar el obstáculo, incrementamos la dificultad
                velocidadBase += 0.2f; 
                velocidadActual = velocidadBase;

                float nuevaX = Random.Range(25f, 45f);
                obstaculos[i].transform.position = new Vector3(nuevaX, -2.2f, 0);
            }
        }
    }
}