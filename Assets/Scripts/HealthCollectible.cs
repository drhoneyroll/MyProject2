using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] int healAmount;
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
            controller.GetComponent<IDamageable>().Heal(healAmount);
            playerBar.Change(healAmount);
            Destroy(gameObject);
        }
    }
}
