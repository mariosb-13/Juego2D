using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Configuración del Mundo")]
    public float velocidadBase = 5f;
    private float velocidadActual = 0f;
    public float retrasoInicio = 1.2f;  
    public Renderer fondo;

    [Header("Prefabs")]
    // ¡IMPORTANTE!: Arrastra los prefabs aquí en el Inspector
    public GameObject col;       
    public GameObject seta;      
    public GameObject tulipan;   
    public GameObject enemigoVolador; 

    [Header("Listas (No tocar)")]
    public List<GameObject> cols = new List<GameObject>();
    public List<GameObject> obstaculos = new List<GameObject>();

    [Header("Interfaz")]
    public TextMeshProUGUI textoPuntos;
    private float puntuacion = 0;

    void Start()
    {
        // 1. Crear el suelo
        for (int i = 0; i < 30; i++)
        {
            cols.Add(Instantiate(col, new Vector2(-15 + i, -3), Quaternion.identity));
        }

        // 2. Crear los primeros obstáculos
        for (int i = 0; i < 6; i++)
        {
            // Decidimos qué obstáculo sacar
            float decision = Random.value;
            GameObject prefabElegido = seta; // Valor por defecto para evitar errores
            float spawnY = -2.2f; // Altura del suelo

            if (decision < 0.5f) // 50% probabilidad de Demonio
            {
                prefabElegido = enemigoVolador;
                // Altura CENTRADA (-0.5 a 0.5) para que la onda lo suba y lo baje sin tocar tierra
                spawnY = Random.Range(-0.5f, 0.5f); 
            }
            else
            {
                prefabElegido = (Random.value > 0.5f) ? seta : tulipan;
            }

            // Lo colocamos lejos
            float spawnX = 20 + (i * 12);
            obstaculos.Add(Instantiate(prefabElegido, new Vector2(spawnX, spawnY), Quaternion.identity));
        }

        Invoke("EmpezarMundo", retrasoInicio);
    }

    void EmpezarMundo() { velocidadActual = velocidadBase; }

    void Update()
    {
        if (velocidadActual > 0)
        {
            puntuacion += Time.deltaTime * 10;
            if (textoPuntos != null) textoPuntos.text = "SCORE: " + Mathf.FloorToInt(puntuacion).ToString();
            
            // Mover fondo
            fondo.material.mainTextureOffset += new Vector2(0.05f, 0) * Time.deltaTime * velocidadActual;
        }

        ActualizarSuelo();
        ActualizarObstaculos();
    }

    void ActualizarSuelo()
    {
        for (int i = 0; i < cols.Count; i++)
        {
            cols[i].transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidadActual;
            if (cols[i].transform.position.x < -15)
            {
                cols[i].transform.position = new Vector3(cols[i].transform.position.x + 30f, -3, 0);
            }
        }
    }

    void ActualizarObstaculos()
    {
        for (int i = 0; i < obstaculos.Count; i++)
        {
            // Mover hacia la izquierda
            obstaculos[i].transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidadActual;

            // Reciclar cuando se sale de la pantalla
            if (obstaculos[i].transform.position.x < -15)
            {
                velocidadBase += 0.2f; // Subimos dificultad
                velocidadActual = velocidadBase;

                // Buscar el último obstáculo para mantener distancia
                float xMasLejana = -15f;
                foreach (GameObject obj in obstaculos)
                {
                    if (obj.transform.position.x > xMasLejana) xMasLejana = obj.transform.position.x;
                }

                // Calcular nueva posición
                float nuevaX = xMasLejana + Random.Range(10f, 18f);

                // Elegir nuevo enemigo
                float decision = Random.value;
                GameObject nuevoPrefab = seta;
                float nuevaY = -2.2f;

                if (decision < 0.5f) 
                {
                    nuevoPrefab = enemigoVolador;
                    nuevaY = Random.Range(-0.5f, 0.5f); // Altura perfecta
                }
                else if (decision < 0.75f)
                {
                    nuevoPrefab = seta;
                }
                else
                {
                    nuevoPrefab = tulipan;
                }

                // Destruir viejo y crear nuevo
                GameObject viejo = obstaculos[i];
                obstaculos[i] = Instantiate(nuevoPrefab, new Vector3(nuevaX, nuevaY, 0), Quaternion.identity);
                Destroy(viejo);
            }
        }
    }
public void DetenerMundo()
{
    velocidadActual = 0f; // Frena el suelo y los enemigos en seco
    velocidadBase = 0f;   // Evita que siga acelerando
}
    
}
