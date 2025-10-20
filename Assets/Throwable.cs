using UnityEngine;


public class Throwable : MonoBehaviour
{
    
    [Header("Configura��es de For�a")]
    public float horizontalForce = 5f; // For�a para a direita
    public float verticalForce = 8f;   // For�a para cima

    [Header("Controle")]
    public KeyCode throwKey = KeyCode.Space; // Tecla para lan�ar

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(throwKey))
        {
            ThrowBall();
        }
    }

    void ThrowBall()
    {
        // Zera velocidade para evitar ac�mulo
        rb.linearVelocity = Vector2.zero;

        // Aplica a for�a como impulso
        rb.AddForce(new Vector2(horizontalForce, verticalForce), ForceMode2D.Impulse);
    }
}
