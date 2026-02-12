using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PantallaInicio : MonoBehaviour
{
    void Update()
    {
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