using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausaUI;
    private bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado) Reanudar();
            else Pausar();
        }
    }

    public void Reanudar()
    {
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f; // El tiempo vuelve a correr
        juegoPausado = false;
    }

    void Pausar()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f; // El tiempo se congela
        juegoPausado = true;
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f; 
        
        SceneManager.LoadScene("MenuPrincipal"); 
    }
}