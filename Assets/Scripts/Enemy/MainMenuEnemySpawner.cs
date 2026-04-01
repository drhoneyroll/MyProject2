using System.Collections;
using System.Threading;
using UnityEngine;

public class MainMenuEnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    public BoxCollider2D spawnArea;

    [SerializeField] private float _timeUntilSpawn=1.5f;

    [SerializeField] private int max_enemies = 5;

    public Canvas CanvasObject;
    private int spawned_enemies = 0;

    void Update()
    {
        _timeUntilSpawn -=  Time.deltaTime;
        if (_timeUntilSpawn <= 0 && spawned_enemies < max_enemies)
        {
            Vector3 randomSpawnPosition = GetRandomPointInCollider(spawnArea);
            Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity, CanvasObject.transform);
            _timeUntilSpawn = 1.5f;
            spawned_enemies += 1;
        }
    }

    Vector3 GetRandomPointInCollider(BoxCollider2D collider)
    {
        float randomX = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
        float randomY = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
        float randomZ = 0;

        return new Vector3(randomX, randomY, randomZ);
    }

}
