using UnityEngine;
using UnityEngine.Rendering;

public class ParallaxManager2D : MonoBehaviour
{

    [SerializeField] private Transform cam; // main camera
    [SerializeField] private Vector3 camStartPos;
    [SerializeField] private float distance;

    [SerializeField] private GameObject[] background;
    [SerializeField] private Material[] mat;
    [SerializeField] private float[] backSpeed;

    float farthestBack;

    [Range(0.01f, 0.5f)]
    public float parallaxSpeed;

    [Range(0.1f, 0.5f)]
    public float speedMultiplier = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;
        int backCount = transform.childCount;
        mat = new Material[backCount];
        background = new GameObject[backCount];

        if (backSpeed == null || backSpeed.Length != backCount)
        {
            backSpeed = new float[backCount];
        }
        for (int i = 0; i < backCount; i++)
        {
            background[i] = transform.GetChild(i).gameObject;
            mat[i] = background[i].GetComponent<Renderer>().material;
        }
        // Calcular automaticamente apenas se backSpeed estiver vazio (todos 0)
        bool needsCalculation = true;
        foreach (float speed in backSpeed)
        {
            if (speed != 0f)
            {
                needsCalculation = false;
                break;
            }
        }
        if (needsCalculation)
        {
            BackSpeedCalculate(backCount);
        }
    }

    void BackSpeedCalculate(int backCount)
    {
        float minZ = float.MaxValue;
        float maxZ = float.MinValue;
        for (int i = 0; i < backCount; i++)
        {
            float z = background[i].transform.position.z;
            if (z < minZ) minZ = z;
            if (z > maxZ) maxZ = z;
        }
        farthestBack = maxZ - minZ;
        for (int i = 0; i < backCount; i++)
        {
            if (farthestBack > 0)
            {
                backSpeed[i] = 1 - ((background[i].transform.position.z - minZ) / farthestBack);
            }
            else
            {
                backSpeed[i] = 1f; 
            }
        }

    }

    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 110);
        for (int i = 0; i < background.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed * speedMultiplier;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}
