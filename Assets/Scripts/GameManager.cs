using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameManager instance;
    ObjectPool deathVFX;
    AudioManager audioManager;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        deathVFX = GetComponent<ObjectPool>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    void Start()
    {
        HealthSystem.onEnemyDeath += OnEnemyDeath;
        HealthSystem.onPlayerDeath += OnPlayerDeath;
    }

    public void OnEnemyDeath(Transform transform)
    {
        GameObject vfx = deathVFX.GetObject();
        vfx.transform.position = transform.position;
        audioManager.PlaySFX(audioManager.death);
        transform.GetComponentInParent<ObjectPool>().ReturnObject(transform.gameObject);

        //Score System

    }

    public void OnPlayerDeath()
    {
        //Game Over
        SceneManager.LoadScene("Game_Level");
    }
}
