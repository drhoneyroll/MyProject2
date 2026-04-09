using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] private float _minimumSpawnTime;
    [SerializeField] private float _maximumSpawnTime;
    [SerializeField] private float _timeUntilSpawn;

    private ObjectPool objectPool;
    [SerializeField] private int maximumSpawnAmount;
    [SerializeField] private int containerEnemyLimit;

    EnemyManager enemyManager;

    void Awake()
    {
        enemyManager = FindAnyObjectByType<EnemyManager>();
        objectPool = GetComponent<ObjectPool>();
        SetTimeUntilSpawn();
    }

    void Start()
    {
        StartCoroutine(CountEvery15Seconds());
    }

    private IEnumerator CountEvery15Seconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            _maximumSpawnTime-=2.5f;
        }
    }

    void Update()
    {
        _timeUntilSpawn -=  Time.deltaTime;

        if (_timeUntilSpawn <= 0)
        {
            if(enemyManager.agents.Count < maximumSpawnAmount)
            {
                GameObject enemy = objectPool.GetObject();
                enemy.transform.position = transform.position;
                enemyManager.agents.Add(enemy.GetComponent<EnemyLogic>());
            }

            SetTimeUntilSpawn();  
        }
    }
    private void SetTimeUntilSpawn()
    {
        _maximumSpawnTime=Mathf.Clamp(_maximumSpawnTime, 1f, 60f);
        _timeUntilSpawn=Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}
