using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] GameObject collectiblePrefab;

    [SerializeField] private float _minimumSpawnTime = 10;
    [SerializeField] private float _maximumSpawnTime = 15;
    [SerializeField] private float _timeUntilSpawn;
    ObjectPool objectPool;

    private Transform randomPosition;

    [SerializeField] Transform spawnContainer;
    Transform[] spawnPoints;

    void Awake()
    {
        SetTimeUntilSpawn();
        spawnPoints = spawnContainer.GetComponentsInChildren<Transform>(true);
        objectPool = GetComponent<ObjectPool>();
    }

    void Update()
    {
        _timeUntilSpawn -=  Time.deltaTime;
        if (_timeUntilSpawn <= 0)
        {
            randomPosition = spawnPoints[Random.Range(0,spawnPoints.Length)];
            GameObject collectable = objectPool.GetObject();
            collectable.transform.position = randomPosition.transform.position;
            SetTimeUntilSpawn();
        }
    }
    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn=Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}
