using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;

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
