using UnityEngine;

public class EnemigoVolador : MonoBehaviour
{
    public float amplitudOnda = 1.5f; 
    public float frecuenciaOnda = 3f;

    private float posicionYInicial;

    void Start()
    {
        posicionYInicial = transform.position.y;
    }

    void LateUpdate()
    {
        float nuevaY = posicionYInicial + Mathf.Sin(Time.time * frecuenciaOnda) * amplitudOnda;

        transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
    }
}