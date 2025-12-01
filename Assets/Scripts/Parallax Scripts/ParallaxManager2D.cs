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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount]; 
        backSpeed = new float[backCount];
        background = new GameObject[backCount];

        for (int i  = 0; i < backCount; i++)
        {
            background[i] = transform.GetChild(i).gameObject;
            mat[i] = background[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {

        for (int i = 0; i < backCount; i++)
        {
            if ((background[i].transform.position.z - cam.position.z) > farthestBack)
                farthestBack = background[i].transform.position.z - cam.position.z;
        }

        for (int i = 0; i < backCount; i++)
            backSpeed[i] = 1 - (background[i].transform.position.z - cam.position.z / farthestBack);
    }

    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 110);
        for (int i = 0; i < background.Length; i++)
        { 
            float speed = backSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0)*speed);
        }
    }
}
