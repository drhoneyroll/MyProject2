using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLogic : StateMachine
{
    [Header("Pathfind Targets")]
    public Transform target;
    public Transform attackPostion;
    public float distanceThreshold;

    [Header("Parametars")]
    public float observeTime = 2f;
    [SerializeField] public float speed = 5f;
    [SerializeField] public float rollSpeed = 2000f;
    [SerializeField] int damageOnCollision = 1;
    [SerializeField] float hitPushBackForce = 200f;
    [SerializeField] float hitStunDuration = 1f;
    public bool isPathfinding = true;
    public bool inRange = false;
    public bool isHit = false;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Vector3 lastAttackPosition;
    [HideInInspector] public Rigidbody2D rb2d;
    [HideInInspector] public EnemyRange enemyRange;
    SpriteRenderer spriteRenderer;

    public Bar playerBar;

    Vector3[] path;
    int targetIndex;


    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public RollAttackState rollAttackState;
    [HideInInspector] public ObserveState observeState;
    [HideInInspector] public HitState hitState;

    [SerializeField] Vector3 gizmoSize = Vector3.one;

    void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>(); 
        animator = transform.GetChild(0).GetComponent<Animator>(); 
        enemyRange = transform.GetChild(1).GetComponent<EnemyRange>(); 
        rb2d = GetComponent<Rigidbody2D>();


        chaseState = new ChaseState(this,"chase");
        observeState = new ObserveState(this,"observe");
        rollAttackState = new RollAttackState(this,"attack");
        hitState = new HitState(this,"hit");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initilize(chaseState);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.LogicUpdate();

        inRange = enemyRange.inRange;

        if(transform.position.x < target.transform.position.x) {
            spriteRenderer.flipX = false;
        } else {
            spriteRenderer.flipX = true;
        }
    }

    void FixedUpdate()
    {
        CurrentState.PhysicsUpdate();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isHit = true;
            Debug.Log("Enemy collided with Player!");
            collision.gameObject.GetComponent<IDamageable>().Damage(damageOnCollision);
            playerBar.Change(-damageOnCollision);
            EnemyPushBackForce();
        }
    }

    public void EnemyPushBackForce()
    {
        rb2d.AddForce((transform.position - target.transform.position).normalized * hitPushBackForce,ForceMode2D.Impulse);
    }

    #region PathFinding
    public Coroutine followPath;

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            if (followPath != null)
                StopCoroutine(followPath);

            isPathfinding = true;
            followPath = StartCoroutine(FollowPath(speed));
        }
    }

    public void OnRollPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            if (followPath != null)
                StopCoroutine(followPath);

            isPathfinding = true;
            followPath = StartCoroutine(FollowPath(rollSpeed));
        }
    }

    IEnumerator FollowPath(float speed)
    {
        if (path == null || path.Length == 0)
        {
            yield break;
        }

        Vector3 currentWaypoint = path[0];
        targetIndex = 0;
        while (true)
        {
            if (Vector3.Distance(transform.position, currentWaypoint) < 0.1f)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    isPathfinding = false;
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            yield return new WaitForFixedUpdate();
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
        }
    }

    public void StopFollowPath()
    {
        if(followPath != null)
        {
            StopCoroutine(followPath);
            path = null;
        }
        isPathfinding = false;
    }

    #endregion

#region EnemyIsHit

    public void HitEnemy()
    {
        StopAllCoroutines();
        ChangeState(hitState);
    }

    public void HitStunOn()
    {
        if(hitStun != null)
            StopCoroutine(HitStun());

        hitStun = StartCoroutine(HitStun());
    }

    public Coroutine hitStun;

    IEnumerator HitStun()
    {
        yield return new WaitForSecondsRealtime(hitStunDuration);
        ChangeState(observeState);
        yield return null;
    }

#endregion

#region ObserveFunction

    public Coroutine observe;

    public void StartObserve()
    {
        if(observe != null)
            StopCoroutine(observe);

        observe = StartCoroutine(Observe());
    }

    IEnumerator Observe()
    {      
        yield return new WaitForSecondsRealtime(observeTime);

        if (inRange)
        {
            ChangeState(rollAttackState);
        } 
        else
        {
            ChangeState(chaseState);
        }
    }

#endregion

    public void RollAttack(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, direction, rollSpeed * Time.deltaTime);
    }
    
    public void OnDrawGizmos()
    {
        if(path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], gizmoSize);

                if(i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i-1],path[i]);
                }
            }
        }
    }
}
