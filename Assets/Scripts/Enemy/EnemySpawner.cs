using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] private float _minimumSpawnTime;

    [SerializeField] private float _maximumSpawnTime;

    [SerializeField] private float _timeUntilSpawn;

    EnemyManager enemyManager;

    private float timer = 0;

    void Awake()
    {
        enemyManager = FindAnyObjectByType<EnemyManager>();
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
            //_minimumSpawnTime+=5;
            _maximumSpawnTime-=2.5f;
            //Debug.Log("15 seconds have passed");
        }
    }

    void Update()
    {
        _timeUntilSpawn -=  Time.deltaTime;
        timer+=Time.deltaTime;
        //Debug.Log("Time "+timer+" moduo: "+(timer % 15));
        /*
        if (timer % 15 <= 0.001)
        {
            Debug.Log("Proslo 15 sekundi: " + timer);
            _minimumSpawnTime+=5;
            _maximumSpawnTime-=5;
        }
        */
        if (_timeUntilSpawn <= 0)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemyManager.agents.Add(enemy.GetComponent<EnemyLogic>());
            SetTimeUntilSpawn();
        }
    }
    private void SetTimeUntilSpawn()
    {
        _maximumSpawnTime=Mathf.Clamp(_maximumSpawnTime, 1f, 60f);
        _timeUntilSpawn=Random.Range(_minimumSpawnTime, _maximumSpawnTime);
        //_maximumSpawnTime=Mathf.Clamp(_maximumSpawnTime, 1f, 2f);
    }
}
