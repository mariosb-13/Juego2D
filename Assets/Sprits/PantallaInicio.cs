using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escenas

public class PantallaInicio : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("SampleScene"); 
        }
    }
}