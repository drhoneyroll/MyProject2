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
        deathVFX = GetComponent<ObjectPool>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        audioManager.PlaySFX(audioManager.death);
        transform.GetComponentInParent<ObjectPool>().ReturnObject(transform.gameObject);
        ScoreManager.instance.AddPoint(10);
        //Score System

    }

    public void OnPlayerDeath()
    {
        audioManager.StopMuisc();
        audioManager.PlaySFX(audioManager.game_over);
        Time.timeScale = 0;
        //Game Over
        //SceneManager.LoadScene("Game_Level");
    }

    public void FreezeTime()
    {
        Time.timeScale = 0;
    }
}
