using UnityEngine;
using UnityEngine.SceneManagement;

public class Throwable : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private string cenaDerrota = "Derrota"; // Nome da cena de derrota

    [Header("Force Settings")]
    private float horizontalForce; // Força no eixo X
    private float verticalForce;   // Força no eixo Y

    [Header("Control")]
    // public GameObject primeiroSkillCheck; // Referência ao primeiro skill check
    [SerializeField] KeyCode throwKey = KeyCode.Space;

    private Rigidbody2D rb;
    public bool isMoving;   // Flag para indicar se está em movimento
    public bool isPlaying;  // Flag para indicar se o jogador está jogando

    public float HorizontalForce { get => horizontalForce; set => horizontalForce = value; }
    public float VerticalForce { get => verticalForce; set => verticalForce = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.useGravity = false;
        isPlaying = false; // Começa como falso
    }




    void Update()
    {
        if (rb != null)
        {
            if (rb.linearVelocity.magnitude > 0.3f)
            {
                if (!isMoving)
                {
                    isMoving = true;
                }
            }
            else
            {
                if (isMoving)
                {
                    isMoving = false;

                    // Se o jogador estava jogando e a bola parou → derrota
                    if (isPlaying)
                    {
                        SceneManager.LoadScene(cenaDerrota);
                    }
                }
            }
        }
    }
    public void ThrowBall()
    {
        rb.linearVelocity = Vector2.zero;
        // rb.linearVelocity = Vector3.zero; // Zera velocidade antes do lançamento
        rb.AddForce(new Vector2(HorizontalForce, VerticalForce), ForceMode2D.Impulse);

        isPlaying = true; // Marca que o jogo começou
        Debug.Log("Bola lançada! isPlaying = true");
    }
}
