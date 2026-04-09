using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject blackScreen;

    public Animator animator;

    public AudioManager audioManager;

    void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    void OnEnable()
    {
        HealthSystem.onPlayerDeath += gameOver;
    }

    void OnDisable()
    {
        HealthSystem.onPlayerDeath -= gameOver;
    }

    public void gameOver ()
    {
        //blackScreen.SetActive(true);
        gameOverUI.SetActive(true);
    }

    public void restart ()
    {
        audioManager.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }

    public void quit ()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
