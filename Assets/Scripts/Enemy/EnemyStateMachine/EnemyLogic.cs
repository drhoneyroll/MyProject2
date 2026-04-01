using System.Collections;
using UnityEngine;

public class EnemyLogic : StateMachine
{

    #region  Parametars
    [Header("Pathfind Targets")]
    public Transform target;
    public Transform attackPostion;
    public float distanceThreshold;

    [Header("Parametars")]
    [SerializeField] private float observeTimeMin = 0.8f;
    [SerializeField] private float observeTimeMax = 2f;
    [SerializeField] private float observeTime;
    [SerializeField] private float rollSpeed = 8f;
    [SerializeField] private int damageOnCollision = 5;
    [SerializeField] private int dmageOnRollCollision = 10;
    [SerializeField] private float hitPushBackForce = 300f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float hitStunDuration = 0.4f;
    public bool isPathfinding = true;
    public bool inRange = false;
    public bool isHit = false;

    [SerializeField] Vector3 gizmoSize = Vector3.one;
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

    public Bar playerBar;
    protected Vector3[] path;
    protected int targetIndex;

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

        chaseState = new ChaseState(this,"chase");
        observeState = new ObserveState(this,"observe");
        rollAttackState = new RollAttackState(this,"attack");
        hitState = new HitState(this,"hit");
    }

    void Start()
    {
        Initilize(chaseState);
        observeTime = Random.Range(observeTimeMin,observeTimeMax);
    }

    void Update()
    {
        CurrentState.LogicUpdate();

        inRange = enemyRange.inRange;

        if(transform.position.x < target.transform.position.x) 
        {
            spriteRenderer.flipX = false;
        } 
        else 
        {
            spriteRenderer.flipX = true;
        }
    }

    void FixedUpdate()
    {
        CurrentState.PhysicsUpdate();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            IDamageable idamage = collision.gameObject.GetComponent<IDamageable>();
            isHit = true;
            if(collision.collider == target.GetComponent<CircleCollider2D>())
            {
                Debug.Log("Enemy collided with Player!");
                if(CurrentState == rollAttackState)
                {
                    idamage.Damage(dmageOnRollCollision);
                    playerBar.Change(-dmageOnRollCollision);
                } 
                else
                {
                    idamage.Damage(damageOnCollision);
                    playerBar.Change(-damageOnCollision);
                }
            }
            EnemyPushBackForce();
        }    
    }

    public void EnemyPushBackForce()
    {
        rb2d.AddForce((transform.position - target.transform.position).normalized * hitPushBackForce,ForceMode2D.Impulse);
    }

    public void RollAttack(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, direction, rollSpeed * Time.deltaTime);
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

    protected IEnumerator FollowPath(float speed)
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

    public void HitEnemy(State _hitState)
    {
        StopAllCoroutines();
        ChangeState(_hitState);
    }

    public Coroutine hitStun;

    public void HitStunOn(State _recoveryState)
    {
        if(hitStun != null)
            StopCoroutine(HitStun(_recoveryState));

        hitStun = StartCoroutine(HitStun(_recoveryState));
    }

    IEnumerator HitStun(State _recoveryState)
    {
        yield return new WaitForSecondsRealtime(hitStunDuration);
        ChangeState(_recoveryState);
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
