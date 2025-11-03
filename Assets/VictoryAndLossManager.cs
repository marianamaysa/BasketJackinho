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
        Debug.Log(isPlaying);
        // Verifica se o jogo está ativo e o objeto está em movimento
        if (isPlaying == true && throwable.isMoving == false)
        {
            SceneManager.LoadScene("Derrota");
            Debug.Log("perdeu");
        }
    }
}
