using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private CameraFollow cameraFollow;

    [Header("Prefabs")]
    [SerializeField] private GameObject collectiblePrefab;
    [SerializeField] private GameObject boostPrefab;

    [Header("Spawn Settings")]
    [SerializeField] float spawnInterval = 1f; // intervalo entre spawns
    [SerializeField] float spawnDistanceAhead = 15f; // distancia a frente da camera
    [SerializeField] float spawnAreaWidth = 8f; // largura da area de spawn
    [SerializeField] float spawnAreaHeight = 3f; // altura da area de spawn
    [SerializeField] int minObjPerSpawn = 2; // minimo de objetos por spawn
    [SerializeField] int maxObjPerSpawn = 5; // maximo de objetos por spawn
    //[SerializeField] float minDistBetweenSpawns = 1f; // distancia minima que a camera se move para spawnar novamente
    [SerializeField] int initialSpawnCount = 30; // qntd de objetos que vao ficar spawnados na cena
    [SerializeField] float speedMultiplier = 0.5f;

    private Camera mainCamera;
    private Transform ballTransform;
    private Rigidbody ballRb;
    //private Vector3 lastSpawnPosition;

    void Start()
    {
        mainCamera = Camera.main;
        //lastSpawnPosition = mainCamera.transform.position;

        if(cameraFollow != null && cameraFollow.target != null)
        {
            ballTransform = cameraFollow.target;
            ballRb = ballTransform.GetComponent<Rigidbody>();
        }
        else
        {
            return;
        }

        // spawn inicial para preencher o mapa com objetos
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnSingleObject(true);
        }

        InvokeRepeating("SpawnObjects", spawnInterval, spawnInterval);
    }

    private void SpawnObjects()
    {
        // verifica se a camera se moveu suficiente pra realizar o spawn
       /*
        if (Vector3.Distance(mainCamera.transform.position, lastSpawnPosition) < minDistBetweenSpawns)
            return;
        */

        //lastSpawnPosition = mainCamera.transform.position; // att a ultima pos

        //Vector3 cameraPos = mainCamera.transform.position;
        //Vector3 spawnBasePos = cameraPos + mainCamera.transform.right * spawnDistanceAhead;

        int numObjects = Random.Range(minObjPerSpawn, maxObjPerSpawn + 1); // num aleatorio de obj a spawnar

        for (int i = 0; i < numObjects; i++)
        {
            SpawnSingleObject(false);
        }
    }

    private void SpawnSingleObject(bool varyDist = false)
    {
        if (ballTransform == null) return;

        // calcula velocidade da bola
        float ballSpeed = ballRb != null ? ballRb.angularVelocity.magnitude : 0f;
        float dynamicDistance = spawnDistanceAhead + (ballSpeed * speedMultiplier);
        float dist = varyDist ? Random.Range(dynamicDistance / 2, dynamicDistance * 1.5f) : dynamicDistance;

        //Vector3 cameraPos = mainCamera.transform.position;
        //float dist = varyDist ? Random.Range(spawnDistanceAhead / 2, spawnDistanceAhead * 2) : spawnDistanceAhead; // varia distancia inicial

        Vector3 ballPos = ballTransform.position;
        Vector3 spawnBasePos = ballPos + Vector3.right * dist;

        // escolhe qual obj spawnar
        GameObject prefabToSpawn = Random.value > 0.5f ? collectiblePrefab : boostPrefab;

        // pos aleatoria dentro da area de spawn
        float groundY = ballPos.y;
        float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        float randomY = Random.Range(0f / 2, spawnAreaHeight);
        Vector3 spawnPos = spawnBasePos + new Vector3(randomX, randomY, 0f);

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}
