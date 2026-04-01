using System.Collections;
using System.Threading;
using Unity.VisualScripting;
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
        //spawnPoints = transform.parent.GetComponentsInChildren<Transform>(true);
        
        spawnPoints = GetComponentsInChildren<Transform>(true);
        Debug.Log(spawnPoints);
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
        //_timeUntilSpawn=Mathf.Clamp(_timeUntilSpawn, 1f, 2f);
    }
}
