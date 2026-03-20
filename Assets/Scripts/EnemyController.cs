using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidbody2d;
    public bool vertical;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    public int damage = 1;
    Animator animator;
    private int current_health;
    public int max_health;
    public int damage_collision=1;
    
    Vector2 move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer<0)
        {
            direction = -direction;
            timer = changeTime;
        }    
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);

        }
        else {
            position.x = position.x + speed * direction * Time.deltaTime;
            
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        
        
        //Vector2 position = (Vector2)rigidbody2d.position + move * movement_speed * Time.deltaTime;
        //Vector2 position = (Vector2)rigidbody2d.position + move * 5.0f * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with Player!");
            collision.gameObject.GetComponent<PlayerController>().ChangeHealth(-damage_collision);
        }
        
        //PlayerController player = other.gameObject.GetComponent<PlayerController>();
        //if (player != null)
        //{
        //    player.ChangeHealth(-1);
        //}
    }

    //public void ChangeHealth(int amount)
    //{
    //    current_health = Mathf.Clamp(current_health + amount, 0, max_health);
    //    if (current_health <= 0)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //    Debug.Log(current_health + "/" + max_health);
    //}
}
