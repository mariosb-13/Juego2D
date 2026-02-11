using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PantallaInicio : MonoBehaviour
{
    void Update()
    {
        // CAMBIO: Ahora solo entra al juego si pulsas ESPACIO o ENTER
        // Así el ratón queda libre para pulsar el botón de Salir sin conflictos
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            EmpezarJuego();
        }
    }

    public void EmpezarJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}