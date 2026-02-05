using UnityEngine;

public class GameManager: MonoBehaviour
{
    public GameObject col;
    public Renderer fondo;
    void Start()
    {
        // Tu plataforma mide 12.53 de ancho según tu foto
        float anchoPlataforma = 12.53f; 

        // Bajamos el número de repeticiones de 21 a 5. 
        // Como son muy largas, con 5 cubres mucho terreno (más de 60 metros).
        for (int i = 0; i < 5; i++)
        {
            // La fórmula mágica: Multiplicamos i * anchoPlataforma
            Instantiate(col, new Vector2(-10f + (i * anchoPlataforma), -3f), Quaternion.identity);
        }
    }

    void Update()
    {
        fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.015f,0f) * Time.deltaTime;
    }
}
