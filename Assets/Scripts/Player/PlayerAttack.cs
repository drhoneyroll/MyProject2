using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    
    [SerializeField] private Transform attackTransform;
    [SerializeField] private LayerMask attackableLayer;

    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float damageAmount = 1f;
    [SerializeField] InputAction inputAction;

    void Awake()
    {
        inputAction.Enable();
    }

    private RaycastHit2D[] hits;
    private void Update()
    {
        if (inputAction.WasPressedThisFrame())
        {
            Attack();
        }
    }

    private void Attack()
    {
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, Vector2.zero , attackableLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            iDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();
            EnemyLogic enemy = hits[i].collider.gameObject.GetComponent<EnemyLogic>();

            if (iDamageable != null)
            {
                iDamageable.Damage(damageAmount);
            }

            if(enemy != null)
            {
                enemy.HitEnemy();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTransform.position,attackRange);
    }
}
