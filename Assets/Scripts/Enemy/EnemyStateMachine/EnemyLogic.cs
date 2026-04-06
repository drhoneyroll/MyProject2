using System.Collections;
using UnityEngine;

public class EnemyLogic : StateMachine
{

    #region  Parametars
    [Header("Pathfind Targets")]
    public Transform target;
    public Transform attackPostion;
    public float distanceThreshold;
    [HideInInspector] public float sqrDistanceThreshold;

    [Header("Observe State")]
    [SerializeField] private float observeTimeMin = 0.8f;
    [SerializeField] private float observeTimeMax = 2f;
    [SerializeField] private float observeTime;

    [Header("Attack Roll")]
    [SerializeField] private float rollSpeed = 8f;
    [SerializeField] private int attackRollDamage = 10;

    [Header("General")]
    [SerializeField] private int collisionDamage = 5;
    [SerializeField] private float hitPushBackForce = 300f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float hitStunDuration = 0.4f;

    [Header("Optimizations")]
    [SerializeField] int frameOffset;

    [Header("Debug")]
    [SerializeField] Vector3 gizmoSize = Vector3.one;
    public float unstuckTimer = 1;
    [Header("Booleans Debug")]
    public bool isPathfinding = true;
    public bool inRange = false;
    public bool isHit = false;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Vector3 lastAttackPosition;
    [HideInInspector] public Rigidbody2D rb2d;
    EnemyRange enemyRange;
    SpriteRenderer spriteRenderer;

    #region States
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public RollAttackState rollAttackState;
    [HideInInspector] public ObserveState observeState;
    [HideInInspector] public HitState hitState;
    #endregion

    [HideInInspector] public Bar playerBar;
    Vector3[] path;
    int targetIndex;

    #endregion

    #region  Cache Values
    CircleCollider2D targetCircleCollider2d;
    IDamageable targetIDamageble;
    #endregion

    void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>(); 
        animator = transform.GetChild(0).GetComponent<Animator>(); 
        enemyRange = transform.GetChild(1).GetComponent<EnemyRange>(); 
        rb2d = GetComponent<Rigidbody2D>();
        playerBar = FindAnyObjectByType<Bar>();
        target = FindAnyObjectByType<PlayerController>().transform;
        attackPostion = FindAnyObjectByType<PlayerController>().transform;
        targetCircleCollider2d = target.GetComponent<CircleCollider2D>();
        targetIDamageble = target.GetComponent<IDamageable>();

        chaseState = new ChaseState(this,"chase");
        observeState = new ObserveState(this,"observe");
        rollAttackState = new RollAttackState(this,"attack");
        hitState = new HitState(this,"hit");
        target = FindAnyObjectByType<PlayerController>().transform;
        attackPostion = FindAnyObjectByType<PlayerController>().transform;
    }

    void OnEnable()
    {
        transform.gameObject.layer = LayerMask.NameToLayer("Attackable");
        Initilize(chaseState);
    }

    void OnDisable()
    {
        isHit = false;
        inRange = false;
        StopAllCoroutines();
    }

    void Start()
    {
        Initilize(chaseState);
        observeTime = Random.Range(observeTimeMin,observeTimeMax);
        sqrDistanceThreshold = distanceThreshold * distanceThreshold;
    }

    void Update()
    {
        //Tick();
    }

    public void Tick()
    {
        inRange = enemyRange.inRange;
        if(transform.position.x < target.transform.position.x) 
        {
            spriteRenderer.flipX = false;
        } 
        else 
        {
            spriteRenderer.flipX = true;
        }
        CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        CurrentState.PhysicsUpdate();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(CurrentState == rollAttackState)
        {
            if (collision.transform == target.transform)
            {
                if(collision.collider == targetCircleCollider2d)
                {
                    targetIDamageble.Damage(attackRollDamage);
                    playerBar.Change(-attackRollDamage);
                }
                isHit = true;
                EnemyPushBackForce();
            }
        }
        else
        {
            if (collision.transform == target.transform)
            {
                if(collision.collider == targetCircleCollider2d)
                {
                    targetIDamageble.Damage(collisionDamage);
                    playerBar.Change(-collisionDamage);
                }
                EnemyPushBackForce();
            }
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

        Vector3 currentWaypoint = path[0];
        targetIndex = 0;
        while (true)
        {
            if ((transform.position - currentWaypoint).sqrMagnitude < 0.01f)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    isPathfinding = false;
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
        }
    }

    public void StopFollowPath()
    {
        if(followPath != null)
        {
            StopCoroutine(followPath);
        }
        path = null;
        isPathfinding = false;
    }

    #endregion

    #region ObserveFunction

    public Coroutine observe;

    public void StartObserve()
    {
        if(observe != null)
        {
            StopCoroutine(observe);
        }
        
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

    #region EnemyIsHit

    public void HitEnemy()
    {
        StopAllCoroutines();
        ChangeState(hitState);
    }

    public Coroutine hitStun;

    public void HitStunOn()
    {
        if(hitStun != null)
            StopCoroutine(HitStun());

        if(gameObject.activeSelf)
            hitStun = StartCoroutine(HitStun());
    }

    IEnumerator HitStun()
    {
        yield return new WaitForSecondsRealtime(hitStunDuration);
        ChangeState(observeState);
        yield return null;
    }

#endregion
    
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
