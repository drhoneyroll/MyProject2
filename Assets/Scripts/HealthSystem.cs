using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHeatlh;
    [SerializeField] float currentHealth;

    public GameManagerScript gameManager;
    public bool HasTakenDamage {get; set;}

    void Awake()
    {
        InitializeHealth();
    }

    public void InitializeHealth()
    {
        currentHealth = maxHeatlh;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void Heal(float healtAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healtAmount,0, maxHeatlh);
    }

    public void Damage(float damageAmount)
    {
        HasTakenDamage = true;
        currentHealth = Mathf.Clamp(currentHealth - damageAmount,0, maxHeatlh);
        if(currentHealth <= 0)
        {
           OnDeath();
        }
    }

    void OnDeath()
    {
        //Invoke Event Manager ; Do Some Behavior
        Destroy(this.gameObject);
        gameManager.gameOver();
    }
}
