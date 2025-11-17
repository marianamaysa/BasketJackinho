using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [Header("Parallax")]
    [SerializeField] GameObject parallaxPrefab; // obj 3d
    [SerializeField] int poolSize = 3; // qntd de instancia
    [SerializeField] float parallaxFactor = 1f; // velocidade
    [SerializeField] Camera mainCam;

    private List<Transform> pool;
    private Vector3 previousCamPos;
    private float prefabWidth = 100;

    void Start()
    {
        if (mainCam == null)
            mainCam = Camera.main;

        // Calcular largura real do prefab considerando todos os renderers filhos
        float totalMinX = float.MaxValue;
        float totalMaxX = float.MinValue;
        foreach (Renderer r in parallaxPrefab.GetComponentsInChildren<Renderer>())
        {
            Bounds b = r.bounds;
            totalMinX = Mathf.Min(totalMinX, b.min.x);
            totalMaxX = Mathf.Max(totalMaxX, b.max.x);
        }
        prefabWidth = totalMaxX - totalMinX;
        Debug.Log("Prefab width combinado: " + prefabWidth);
        pool = new List<Transform>(poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            Vector3 pos = transform.position + new Vector3(i * prefabWidth, 0, 0);
            GameObject go = Instantiate(parallaxPrefab, pos, Quaternion.Euler(0f, -90f, 0f), transform);
            pool.Add(go.transform);
        }

        previousCamPos = mainCam.transform.position;
    }

    void Update()
    {
        Vector3 camDelta = mainCam.transform.position - previousCamPos;

        foreach (Transform t in pool)
            t.position += new Vector3(camDelta.x * parallaxFactor, camDelta.y * parallaxFactor, 0);

        // reposiciona itens que estao atras
        Transform leftmost = FindLeftmost();
        if (mainCam.transform.position.x - leftmost.position.x > prefabWidth)
        {
            // Move a instância mais à esquerda para a direita do pool
            leftmost.position += new Vector3(prefabWidth * poolSize, 0, 0);
        }

        previousCamPos = mainCam.transform.position;
    }

    Transform FindLeftmost()
    {
        Transform leftmost = pool[0];
        foreach (Transform t in pool)
        {
            if (t.position.x < leftmost.position.x)
                leftmost = t;
        }
        return leftmost;
    }
}
