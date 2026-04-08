using UnityEngine;
using System;
using NUnit.Framework.Constraints;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHeatlh;
    [SerializeField] float currentHealth;
    public bool HasTakenDamage { get; set;}
    public static event Action onPlayerDeath;
    public static event Action<Transform> onEnemyDeath;

    public

    void Awake()
    {
        InitializeHealth();
    }

    void OnEnable()
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
            if (transform.CompareTag("Player"))
            {
                //Player Death
                onPlayerDeath?.Invoke();
                return;
            }

            //On Enemy Death Logic
            onEnemyDeath?.Invoke(transform);
            
        }
    }
}
