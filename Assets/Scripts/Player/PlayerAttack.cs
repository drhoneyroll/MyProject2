using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float damageAmount = 1f;
    [SerializeField] InputAction inputAction;

    void Awake()
    {
        inputAction.Enable();
    }

    private RaycastHit2D[] hits;

    private Animator anim;

    private void Start()
    {
        anim=GetComponent<Animator>();
    }

    private void Update()
    {
        if (inputAction.WasPressedThisFrame())
        {
            Attack();
            anim.SetTrigger("punch");
        }
    }

    private void Attack()
    {
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

            if (iDamageable != null)
            {
                iDamageable.Damage(damageAmount);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTransform.position,attackRange);
    }
}
