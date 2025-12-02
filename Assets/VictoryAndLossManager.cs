using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryAndLossManager : MonoBehaviour
{
    public bool isPlaying;
    private Throwable throwable;

    public bool IsPlaying { get => isPlaying; set => isPlaying = value; }

    private void Start()
    {
        IsPlaying = false;
    }

    private void Update()
    {
        // Verifica se o jogo está ativo e o objeto está em movimento
        if (isPlaying == true && throwable.isMoving == false)
        {
            SceneManager.LoadScene("Derrota");
            Debug.Log("perdeu");
        }
    }
        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SceneManager.LoadScene("Vitoria");
            Debug.Log("Ganhou");
        }
    }
}

