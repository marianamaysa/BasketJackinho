using UnityEngine;


public class Throwable : MonoBehaviour
{
    [Header("Force Settings")]
    private float horizontalForce; // Forca para a direita (eixo X)
    private float verticalForce;   // Forca para cima (eixo Y)

    [Header("Control")]
    public GameObject primeiroSkillCheck; // Ref ao primeiro skill check (vertical) para ativa-lo ao pressionar Space

    private Rigidbody rb;
    public float HorizontalForce { get => horizontalForce; set => horizontalForce = value; }
    public float VerticalForce { get => verticalForce; set => verticalForce = value; }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Ao pressionar Space, ativa o primeiro skill check (vertical)
        {
            if (primeiroSkillCheck != null)
            {
                primeiroSkillCheck.SetActive(true);
            }
        }
    }
    public void ThrowBall()
    {
        rb.useGravity = true; // Ativa a gravidade apenas no lançamento
        rb.linearVelocity = Vector3.zero; // Zera velocidade para evitar acumulo
        rb.AddForce(new Vector3(horizontalForce, verticalForce, 0), ForceMode.Impulse); // Aplica a força como impulso
    }
}
