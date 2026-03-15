using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidbody2d;
    public bool vertical;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;

    
    Vector2 move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
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

        }
        else {
            position.x = position.x + speed * direction * Time.deltaTime;
        }
        
        
        //Vector2 position = (Vector2)rigidbody2d.position + move * movement_speed * Time.deltaTime;
        //Vector2 position = (Vector2)rigidbody2d.position + move * 5.0f * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
}
