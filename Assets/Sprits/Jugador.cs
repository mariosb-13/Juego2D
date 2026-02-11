using UnityEngine;

public class Jugador : MonoBehaviour
{
    [Header("Configuración de Salto")]
    public float fuerzaSalto = 750f; 

    [Header("Conexiones")]
    public GameObject pantallaGameOver; 
    public AudioSource musicaFondo;    

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource sonidoChoque; 
    private bool tocandoSuelo = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sonidoChoque = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        // DETECCIÓN MÓVIL + PC: Espacio o cualquier toque en pantalla
        bool quiereSaltar = Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);

        // Salto: Solo si toca suelo y el juego no está en pausa
        if (quiereSaltar && tocandoSuelo && Time.timeScale == 1)
        {
            // Reset de velocidad para saltos más consistentes
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); 
            rb.AddForce(new Vector2(0f, fuerzaSalto));
            
            // ACTIVAR ANIMACIÓN: Forzamos el estado y reiniciamos el clip
            animator.SetBool("estaSaltando", true);
            animator.Play("Saltar", -1, 0f); 
            
            tocandoSuelo = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            animator.SetBool("estaSaltando", false); 
            tocandoSuelo = true; 
        }

        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            Morir();
        }
    }

    void Morir()
    {
        if (musicaFondo != null) musicaFondo.Stop(); 
        if (sonidoChoque != null) sonidoChoque.Play();

        if (pantallaGameOver != null) pantallaGameOver.SetActive(true);

        Time.timeScale = 0; 
    }
}