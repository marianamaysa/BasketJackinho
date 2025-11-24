using UnityEngine;

public class Parallax2D : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] private Renderer bgRender;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bgRender.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
