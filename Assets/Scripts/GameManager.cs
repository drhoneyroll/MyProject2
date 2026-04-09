using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    ObjectPool deathVFX;
    AudioManager audioManager;

    
    void Awake()
    {
        // Singleton Pattern - Nece se unistiti objekat on sceneload - Keep in Mind.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        DontDestroyOnLoad(gameObject); 
    }

    void Start()
    {
        HealthSystem.onEnemyDeath += OnEnemyDeath;
        HealthSystem.onPlayerDeath += OnPlayerDeath;
        Coins.onPickUp += OnCoinPickUp;
        deathVFX = GetComponent<ObjectPool>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        HealthSystem.onEnemyDeath -= OnEnemyDeath;
        HealthSystem.onPlayerDeath -= OnPlayerDeath;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        Time.timeScale = 1;
        //OnScene Load Do Something
    }

    public void OnEnemyDeath(Transform transform)
    {
        GameObject vfx = deathVFX.GetObject();
        vfx.transform.position = transform.position;
        audioManager.StopSFX();
        audioManager.PlaySFX(audioManager.death);
        transform.GetComponentInParent<ObjectPool>().ReturnObject(transform.gameObject);
        ScoreManager.instance.AddPoint(10);
        //Score System

    }

    public void OnPlayerDeath()
    {
        audioManager.musicSource.Stop();
        audioManager.PlaySFX(audioManager.game_over_death);
        Debug.Log("audio manager components: "+audioManager.GetComponentInChildren<AudioSource>());
        Time.timeScale = 0;
        //Game Over
        //SceneManager.LoadScene("Game_Level");
    }

    public void OnCoinPickUp()
    {
        ScoreManager.instance.AddPoint(50);
        audioManager.PlaySFX(audioManager.health_pickup);
    }
}
