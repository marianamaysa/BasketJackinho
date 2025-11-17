using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Points")]
    public int totalPoints = 0; // Ponto total do jogador
    public Text pointsText; // Ref a texto de UI para mostrar os pontos


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        // Inicializa o texto de pontos desde o início
        UpdatePointsText();
    }


    // Add pontod
    public void AddPoints(int points)
    {
        totalPoints += points;
        Debug.Log("Pontos totais: " + totalPoints);

        // Att a UI
        if (pointsText != null)
            pointsText.text = "Pontos: " + totalPoints.ToString();
    }
    private void UpdatePointsText()
    {
        if (pointsText != null)
            pointsText.text = "Pontos: " + totalPoints.ToString();
    }
}
