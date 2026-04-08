using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;

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
        gameOverUI.SetActive(true);
    }

    public void restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }

    public void quit ()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
