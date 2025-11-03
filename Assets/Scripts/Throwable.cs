using UnityEngine;
using UnityEngine.SceneManagement;

public class Throwable : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private string cenaDerrota = "Derrota"; // Nome da cena de derrota

    [Header("Force Settings")]
    [SerializeField] private float horizontalForce; // Força no eixo X
    [SerializeField] private float verticalForce;   // Força no eixo Y
    [SerializeField] private float forceMultiplier = 3.5f;

    [Header("Control")]
    public GameObject primeiroSkillCheck; // Referência ao primeiro skill check

    private Rigidbody rb;
    public bool isMoving;   // Flag para indicar se está em movimento
    public bool isPlaying;  // Flag para indicar se o jogador está jogando

    public float HorizontalForce { get => horizontalForce; set => horizontalForce = value; }
    public float VerticalForce { get => verticalForce; set => verticalForce = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        isPlaying = false; // Começa como falso
    }

    void Update()
    {
        // Ativa o skill check ao pressionar Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (primeiroSkillCheck != null)
                primeiroSkillCheck.SetActive(true);
        }

        // Verifica se o objeto está em movimento
        if (rb != null)
        {
            if (rb.linearVelocity.magnitude > 0.3f)
            {
                if (!isMoving)
                {
                    isMoving = true;
                    Debug.Log("🎯 Objeto começou a se mover!");
                }
            }
            else
            {
                if (isMoving)
                {
                    isMoving = false;
                    Debug.Log("🛑 Objeto parou de se mover!");

                    // Se o jogador estava jogando e a bola parou → derrota
                    if (isPlaying)
                    {
                        Debug.Log("💀 Jogo acabou! Carregando cena de derrota...");
                        SceneManager.LoadScene(cenaDerrota);
                    }
                }
            }
        }
    }

    public void ThrowBall()
    {
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero; // Zera velocidade antes do lançamento
        rb.AddForce(new Vector3(horizontalForce, verticalForce, 0) * forceMultiplier, ForceMode.Impulse);

        isPlaying = true; // Marca que o jogo começou
        Debug.Log("🚀 Bola lançada! isPlaying = true");
    }
}
