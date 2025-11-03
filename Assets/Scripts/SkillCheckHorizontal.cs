using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillCheckHorizontal : MonoBehaviour
{
    public Throwable throwable;
    [Header("Refs UI")]
    public RectTransform barraMovel;
    public RectTransform areaTotal;

    [Header("Settings")]
    public float velocidade = 200f;
    public float tempoEsperaAntesDeEncerrar = 0.5f;

    [Header("Force (0 a 1 da altura total)")]
    [Range(0f, 2f)] public float limiteFracoMax = 0.3f;
    [Range(0f, 2f)] public float limiteMedioMin = 0.3f;
    [Range(0f, 2f)] public float limiteMedioMax = 0.7f;
    [Range(0f, 2f)] public float limiteForteMin = 0.45f;
    [Range(0f, 5f)] public float limiteForteMax = 0.55f;

    [Header("Result")]
    public int valorForca;

    private bool indoDireita = true;  // Ajustado para movimento horizontal
    private bool ativo = true;

    private float larguraMin;  // Ajustado para largura
    private float larguraMax;

    void Start()
    {
        float halfBarheight = barraMovel.rect.height / 2f;
        larguraMin = -((areaTotal.rect.height / 2f) - halfBarheight);
        larguraMax = ((areaTotal.rect.height / 2f) - halfBarheight);
    }

    void Update()
    {
        if (!ativo) return;

        MoverBarra();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ativo = false;
            DetectarForca();
            StartCoroutine(EncerrarSkillCheck());
        }
    }

    void MoverBarra()
    {
        float movimento = velocidade * Time.deltaTime;
        Vector2 pos = barraMovel.anchoredPosition;

        pos.y += indoDireita ? movimento : -movimento;

        if (pos.y >= larguraMax)
        {
            pos.y = larguraMax;
            indoDireita = false;
        }
        else if (pos.y <= larguraMin)
        {
            pos.y = larguraMin;
            indoDireita = true;
        }

        barraMovel.anchoredPosition = pos;
    }

    void DetectarForca()
    {
        float proporcao = Mathf.InverseLerp(larguraMin, larguraMax, barraMovel.anchoredPosition.y);

        if (proporcao >= limiteForteMin && proporcao <= limiteForteMax)
            valorForca = 10;
        else if (proporcao >= limiteMedioMin && proporcao <= limiteMedioMax)
            valorForca = 5;
        else
            valorForca = 2;

        Debug.Log("Força vertical: " + valorForca);
    }

    IEnumerator EncerrarSkillCheck()
    {
        throwable.HorizontalForce = valorForca;
        Debug.Log(valorForca);
        throwable.ThrowBall();
        yield return new WaitForSeconds(tempoEsperaAntesDeEncerrar);
        gameObject.SetActive(false);
    }
}