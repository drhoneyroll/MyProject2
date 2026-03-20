using System.Collections;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLogic : StateMachine
{

    public Animator animator;
    public Transform target;
    public Transform attackPostion;

    public float observeTime = 2f;
    [SerializeField] int damage_collision = 1;

    public bool inRange;
    public Vector3 lastAttackPosition;
    public float distanceThreshold;
    Rigidbody2D rb2d;

    [SerializeField] float speed = 5f;
    Vector3[] path;
    int targetIndex;
    
    SpriteRenderer spriteRenderer;

    public ChaseState chaseState;
    public ObserveState observeState;
    public AttackState attackState;
    public BlockState blockState;

    [SerializeField] Vector3 gizmoSize = Vector3.one;

    void Awake()
    {
        spriteRenderer =GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        chaseState = new ChaseState(this,"chase");
        observeState = new ObserveState(this,"observe");
        attackState = new AttackState(this,"attack");
        blockState = new BlockState(this,"block");
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == target.gameObject)
        {
            inRange = true;
            ChangeState(observeState);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == target.gameObject)
        {
            inRange = false;
            ChangeState(chaseState);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with Player!");
            collision.gameObject.GetComponent<PlayerController>().ChangeHealth(-damage_collision);
        }
    }

    #region PathFinding
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        targetIndex = 0;
        while (true)
        {
            if(transform.position == currentWaypoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            yield return new WaitForFixedUpdate();
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    #endregion


    IEnumerator BlockAttack()
    {
        float timer = observeTime;
        while (timer > 0)
        {
            //Detect if player is attacking and change the state to Block

            yield return new WaitForSecondsRealtime(1);
            timer -= 1;
        }
        ChangeState(attackState);
        yield return null;
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
