using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int incr;
    Bar playerBar;

    void Awake()
    {
        playerBar = FindAnyObjectByType<Bar>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object that entered the trigger: " + other);
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller !=null )
        {
            controller.ChangeHealth(incr);
            playerBar.Change(incr);
            Destroy(gameObject);
        }
    }
}
