using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string cenaACarregar;
    public void TrocarCena()
    {
        SceneManager.LoadScene(cenaACarregar);
    }

    public void SairJogo()
    {
        Application.Quit();
    }
}
