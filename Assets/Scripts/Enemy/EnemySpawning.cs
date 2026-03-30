using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class EnemySpawning : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    //[SerializeField] private GameObject PathfindingEnemy;
    private float game_time = 0;

    public float interval = 5f;


    void Start()
    {
        //game_time=0f;
        StartCoroutine(SpawnEnemy(interval,enemyPrefab));
    }

    private IEnumerator SpawnEnemy (float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        if (game_time % 30f == 0)
        {
            Debug.Log("game time: " + game_time);
            interval -= 1f;
            interval=Mathf.Clamp(interval, 1f, 5f);
        }
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f,5), Random.Range(-6f,6f),0), Quaternion.identity);
        
        StartCoroutine(SpawnEnemy(interval,enemy));
    }
}
