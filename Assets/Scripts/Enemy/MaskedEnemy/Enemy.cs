using UnityEngine;
using System.Collections;

public class Enemy : StateMachine
{
    #region  Parametars
    [Header("Pathfind Targets")]
    public Transform target;
    public Transform attackPostion;
    public float distanceThreshold;

    [Header("Parametars")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float hitStunDuration = 0.4f;
    public bool isPathfinding = true;
    public bool inRange = false;

    [SerializeField] Vector3 gizmoSize = Vector3.one;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Vector3 lastAttackPosition;
    [HideInInspector] public Rigidbody2D rb2d;
    EnemyRange enemyRange;
    SpriteRenderer spriteRenderer;

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
    }

    void Update()
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
