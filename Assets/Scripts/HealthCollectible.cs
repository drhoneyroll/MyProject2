using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] int healAmount;
    Bar playerBar;

    AudioManager audio_manager;
    ObjectPool objectPool;

    void Awake()
    {
        playerBar = FindAnyObjectByType<Bar>();
        objectPool = GetComponentInParent<ObjectPool>();
    }

    void Start()
    {
        audio_manager=FindAnyObjectByType<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller !=null )
        {
            controller.GetComponent<IDamageable>().Heal(healAmount);
            playerBar.Change(healAmount);
            audio_manager.PlaySFX(audio_manager.health_pickup);
            objectPool.ReturnObject(this.gameObject);
        }
    }
}
