using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Config")]
    public int pointsValue = 10; // Pontos ao coletar o item

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
            Collect(); // Coleta o item e destroi o objeto
    }

    private void Collect()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.AddPoints(pointsValue);
        }
        else
        {
            Debug.Log("GameManager não encontrado");
        }

        Destroy(gameObject);
    }
}
