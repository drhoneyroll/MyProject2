using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] GameObject collectiblePrefab;

    [SerializeField] private float _minimumSpawnTime = 10;
    [SerializeField] private float _maximumSpawnTime = 15;
    [SerializeField] private float _timeUntilSpawn;

    //private GameObject[] spawnPoints;
    public GameObject spawner;
    private Transform randomPosition;

    Transform[] spawnPoints;

    void Awake()
    {
        SetTimeUntilSpawn();
        spawnPoints = GetComponentsInChildren<Transform>(true);
    }

    void Update()
    {
        _timeUntilSpawn -=  Time.deltaTime;
        if (_timeUntilSpawn <= 0)
        {
            randomPosition = spawnPoints[Random.Range(0,spawnPoints.Length)];
            Instantiate(collectiblePrefab, randomPosition.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }
    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn=Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}
