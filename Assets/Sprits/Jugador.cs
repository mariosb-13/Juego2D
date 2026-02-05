using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float fuerzaSalto;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("estaSaltando", true);
            rigidbody2D.AddForce(new Vector2(0f, fuerzaSalto));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "Suelo")
       {
            animator.SetBool("estaSaltando", false);
       } 
    }
}
