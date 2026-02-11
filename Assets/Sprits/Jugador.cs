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
        // Salto: Solo si toca suelo y el juego no está en pausa
        if (Input.GetKeyDown(KeyCode.Space) && tocandoSuelo && Time.timeScale == 1)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); 
            rb.AddForce(new Vector2(0f, fuerzaSalto));
            
            animator.SetBool("estaSaltando", true);
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