using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaInicio : MonoBehaviour
{
    void Update()
    {
        bool tocarPantalla = Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
        bool teclaPC = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return);

        if (teclaPC || tocarPantalla)
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