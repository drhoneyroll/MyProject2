using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerController : MonoBehaviour

{
    Rigidbody2D rigidbody2d;
    public int maxHealth = 5;
    public int currentHealth=3;
    Vector2 move;
    public float movement_speed;
    public InputAction MoveAction;
    public Animator animator;

    [SerializeField] public InputAction AttackAction;

    //public PlayerAttack_improvment player_Combat;


    Vector2 moveDirection = new Vector2(0, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        AttackAction.Enable();
    }
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate=10;
        //currentHealth = maxHealth;
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("horizontal", 0f);
        animator.SetFloat("vertical", 0f);

    }

    // Update is called once per frame
    void Update()
    {
        /*if (AttackAction.WasPressedThisFrame())
        {
            player_Combat.Attack();
        }
        //Debug.Log(move);
        */

        if (move.magnitude <= 0.1)
        {
            animator.SetFloat("Speed", 0f);
        }
    }
    void FixedUpdate()
    {
        /*
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);
        */

        move = MoveAction.ReadValue<Vector2>();
        //Debug.Log("move "+move);
        if(!Mathf.Approximately(move.x,0.0f) || !Mathf.Approximately(move.y,0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        else
        {
            moveDirection.Set(0f,0f); //bez ovoga ima da loopuje infinite u animaciji
        }

        if (moveDirection.x > 0 && transform.localScale.x < 0 ||
                moveDirection.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        animator.SetFloat("horizontal", Mathf.Abs(moveDirection.x));
        animator.SetFloat("vertical", Mathf.Abs(moveDirection.y));
        //animator.SetFloat("horizontal", moveDirection.x);
        //animator.SetFloat("vertical", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = (Vector2)rigidbody2d.position + move * movement_speed * Time.deltaTime;

        //Vector2 position = (Vector2)rigidbody2d.position + move * 5.0f * Time.deltaTime;

        rigidbody2d.MovePosition(position);
        //animator.SetFloat("horizontal", 0f);
        //animator.SetFloat("vertical", 0f);
    }
    
    void Flip()
    {
        //facingDirection *= -1;
        transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        //currentHealth+=amount;
        //Debug.Log(currentHealth + "/" + maxHealth);

        if (currentHealth<=0)
        {
            gameObject.SetActive(false);
        }
    }

    

}
