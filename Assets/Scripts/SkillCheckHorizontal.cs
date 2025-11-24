using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillCheckHorizontal : MonoBehaviour
{
    public Throwable throwable;
    [Header("Referências UI")]
    public RectTransform barraMovel;
    public RectTransform areaTotal;

    [Header("Configurações")]
    public float velocidade = 200f;
    public float tempoEsperaAntesDeEncerrar = 0.5f;

    [Header("Faixas de Força (0 a 1 da altura total)")]
    [Range(0f, 1f)] public float limiteFracoMax = 0.3f;
    [Range(0f, 1f)] public float limiteMedioMin = 0.3f;
    [Range(0f, 1f)] public float limiteMedioMax = 0.7f;
    [Range(0f, 1f)] public float limiteForteMin = 0.45f;
    [Range(0f, 1f)] public float limiteForteMax = 0.55f;

    [Header("Resultado")]
    public int valorForca;

    private bool subindo = true;
    private bool ativo = true;

    private float alturaMin;
    private float alturaMax;

    void Start()
    {
        float halfBarHeight = barraMovel.rect.height / 2f;
        alturaMin = -((areaTotal.rect.height / 2f) - halfBarHeight);
        alturaMax = ((areaTotal.rect.height / 2f) - halfBarHeight);
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

        pos.y += subindo ? movimento : -movimento;

        if (pos.y >= alturaMax)
        {
            pos.y = alturaMax;
            subindo = false;
        }
        else if (pos.y <= alturaMin)
        {
            pos.y = alturaMin;
            subindo = true;
        }

        barraMovel.anchoredPosition = pos;
    }

    void DetectarForca()
    {
        float proporcao = Mathf.InverseLerp(alturaMin, alturaMax, barraMovel.anchoredPosition.y);

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