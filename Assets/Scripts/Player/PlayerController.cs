using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour

{
    Rigidbody2D rigidbody2d;
    public float movement_speed;
    public InputAction MoveAction;
    public Animator animator;

    [SerializeField] public InputAction AttackAction;

    //public PlayerAttack_improvment player_Combat;

    Vector2 move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        AttackAction.Enable();
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.SetFloat("horizontal", 0f);
        animator.SetFloat("vertical", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        if (move.x > 0 && transform.localScale.x < 0 ||
            move.x < 0 && transform.localScale.x > 0)
        {           
            Flip();
        }

        animator.SetFloat("horizontal", Mathf.Abs(move.x));
        animator.SetFloat("vertical", Mathf.Abs(move.y));
        animator.SetFloat("Speed", move.magnitude);
    }
    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * movement_speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
    
    void Flip()
    {
        transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}