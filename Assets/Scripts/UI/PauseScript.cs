using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScript : MonoBehaviour
{
    public InputAction pause;
    bool player_dead;

    public GameObject gameUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        pause.Enable();
        player_dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("update u pause script");
        if (pause.WasPressedThisFrame() && !player_dead)
        {
            Debug.Log("escape was pressed");
            if (!gameUI.activeSelf)
            {
                Debug.Log("pause");
                Pause();
            }
            else
            {
                Debug.Log("unpause");
                Unpause();
            }
        }
    }

    public void Pause()
    {
        gameUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        gameUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void DisablePauseInput()
    {
        pause.Disable();
    }

    void OnEnable()
    {
        HealthSystem.onPlayerDeath += DisablePauseInput;
    }

    void OnDisable()
    {
        HealthSystem.onPlayerDeath -= DisablePauseInput;
    }
}
