using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject collectiblePrefab;
    [SerializeField] private GameObject boostPrefab;

    [Header("Spawn Settings")]
    [SerializeField] float spawnInterval = 2f; // intervalo entre spawns
    [SerializeField] float spawnDistanceAhead = 10f; // distancia a frente da camera
    [SerializeField] float spawnAreaWidth = 5f; // largura da area de spawn
    [SerializeField] float spawnAreaHeight = 3f; // altura da area de spawn
    [SerializeField] int minObjPerSpawn = 1; // minimo de objetos por spawn
    [SerializeField] int maxObjPerSpawn = 3; // maximo de objetos por spawn
    [SerializeField] float minDistBetweenSpawns = 5f; // distancia minima que a camera se move para spawnar novamente

    private Camera mainCamera;
    private Vector3 lastSpawnPosition;

    void Start()
    {
        mainCamera = Camera.main;
        lastSpawnPosition = mainCamera.transform.position;
        InvokeRepeating("SpawnObjects", spawnInterval, spawnInterval);
    }

    private void SpawnObjects()
    {
        // verifica que a camera se moveu suficiente pra realizar o spawn
        if (Vector3.Distance(mainCamera.transform.position, lastSpawnPosition) < minDistBetweenSpawns)
            return;

        lastSpawnPosition = mainCamera.transform.position; // att a ultima pos

        Vector3 cameraPos = mainCamera.transform.position;
        Vector3 spawnBasePos = cameraPos + mainCamera.transform.right * spawnDistanceAhead;

        int numObjects = Random.Range(minObjPerSpawn, maxObjPerSpawn + 1); // num aleatorio de obj a spawnar

        for (int i = 0; i < numObjects; i++)
        {
            // escolhe entre coletavel e boost
            GameObject prefabToSpawn = Random.value > 0.5f ? collectiblePrefab : boostPrefab;

            // pos aleatoria dentro da area de spawn
            float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
            float randomY = Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2);
            Vector3 spawnPos = spawnBasePos + new Vector3(randomX, randomY, 0f);

            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        }
    }
}
