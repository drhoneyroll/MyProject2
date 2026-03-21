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
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate=10;
        //currentHealth = maxHealth;
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        if(!Mathf.Approximately(move.x,0.0f) || !Mathf.Approximately(move.y,0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        //Debug.Log(move);
    }
    void FixedUpdate()
    {
    Vector2 position = (Vector2)rigidbody2d.position + move * movement_speed * Time.deltaTime;
    //Vector2 position = (Vector2)rigidbody2d.position + move * 5.0f * Time.deltaTime;
    rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        //currentHealth+=amount;
        Debug.Log(currentHealth + "/" + maxHealth);

        if (currentHealth<=0)
        {
            gameObject.SetActive(false);
        }
    }
}
