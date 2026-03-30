using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack_improvment : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float damageAmount = 1f;
    [SerializeField] InputAction inputAction;
    private RaycastHit2D[] hits;
    [SerializeField] private float timeBtwAttacks = 0.15f;
    public bool ShouldBeDamaging {get; private set; } = false;
    private float attackTimeCounter;
    private List<HealthSystem> iDamageables = new List<HealthSystem>();

    void Awake()
    {
        inputAction.Enable();
    }

    private void Start()
    {
        anim=GetComponent<Animator>();
        attackTimeCounter = timeBtwAttacks;
    }

    private void Update()
    {
        if (inputAction.WasPressedThisFrame() && attackTimeCounter >= timeBtwAttacks)
        {
            Attack();
            attackTimeCounter = 0f;
        }

        attackTimeCounter += Time.deltaTime;
    }

    public void Attack()
    {
        anim.SetBool("IsAttacking",true);      
    }

    public void FinishAttacking()
    {
        anim.SetBool("IsAttacking",false);
    }

    public IEnumerator DamageWhileSlashIsActive()
    {
        ShouldBeDamaging = true;
        while(ShouldBeDamaging)
        {
            Debug.Log("Im in while loop");
            hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);

            for (int i = 0; i < hits.Length; i++) //vrti kroz sve targete zahvacene CircleCastom
            {
                Debug.Log("Iteracija for petlje"+i+' '+hits.Length);
                HealthSystem iDamageable = hits[i].collider.gameObject.GetComponent<HealthSystem>();
                EnemyLogic enemyLogic = hits[i].collider.gameObject.GetComponent<EnemyLogic>();            

                if (iDamageable != null && !iDamageable.HasTakenDamage)
                {
                    Debug.Log("Damage?");
                    iDamageable.Damage(damageAmount);
                    iDamageables.Add(iDamageable);
                }

                if(enemyLogic != null)
                {
                    enemyLogic.HitEnemy(enemyLogic.hitState);
                }

            }
            Debug.Log("Should Be Damaging: "+ ShouldBeDamaging);
            yield return null; //wait for one more frame (game will freeze without this)
        }

        ReturnAttackablesToDamageable();
    }

    private void ReturnAttackablesToDamageable()
    {
        Debug.Log("ReturnAttackablesToDamageable");
        foreach (IDamageable thingThatWasDamaged in iDamageables)
        {
            Debug.Log("thingThatWasDamaged: " + thingThatWasDamaged + " " + thingThatWasDamaged.HasTakenDamage);
            thingThatWasDamaged.HasTakenDamage = false;
            Debug.Log("thingThatWasDamaged: " + thingThatWasDamaged + " " + thingThatWasDamaged.HasTakenDamage);
        }

        iDamageables.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTransform.position,attackRange);
    }

    #region Animation Triggers

    public void ShouldBeDamagingToTrue()
    {
        ShouldBeDamaging = true;
    }

    public void ShouldBeDamagingToFalse()
    {
        ShouldBeDamaging = false;
    }

    #endregion

}
