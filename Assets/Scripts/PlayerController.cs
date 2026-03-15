using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerController : MonoBehaviour

{
    Rigidbody2D rigidbody2d;
    public int maxHealth = 5;
    int currentHealth=1;
    Vector2 move;
    public float movement_speed;
    public InputAction MoveAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate=10;
        //currentHealth = maxHealth;
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
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
        Debug.Log(currentHealth + "/" + maxHealth);
    }

}
