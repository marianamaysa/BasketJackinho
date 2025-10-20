using UnityEngine;


public class Throwable : MonoBehaviour
{

    [Header("Configura��es de For�a")]
    private float horizontalForce; // For�a para a direita 
    private float verticalForce;   // For�a para cima

    [Header("Controle")]
    public KeyCode throwKey = KeyCode.Space; // Tecla para lan�ar

    private Rigidbody2D rb;

    public float HorizontalForce { get => horizontalForce; set => horizontalForce = value; }
    public float VerticalForce { get => verticalForce; set => verticalForce = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ThrowBall()
    {
        // Zera velocidade para evitar ac�mulo
        rb.linearVelocity = Vector2.zero;

        // Aplica a for�a como impulso
        rb.AddForce(new Vector2(HorizontalForce, VerticalForce), ForceMode2D.Impulse);
    }
}
