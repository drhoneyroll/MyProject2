using System.Collections;
using UnityEngine;
using UnityEngine.U2D.IK;

public class WeaponSaw : MonoBehaviour
{

    Vector2 direction;
    Rigidbody2D mybody;
    CircleCollider2D myCollider;
    [SerializeField] Transform weaponUser;
    [SerializeField] Transform target;
    [SerializeField] float speed = 2;
    bool hit = false;

    void Awake()
    {
        myCollider = GetComponent<CircleCollider2D>();
        mybody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        direction = (target.transform.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        if (!hit)
        {
            mybody.linearVelocity = direction * speed * Time.deltaTime;
            mybody.angularVelocity = 45f * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            
        } 
        else
        {
            hit = true;
            StartCoroutine(nameof(HitGround));
        }
    }

    void OnDisable()
    {
        myCollider.enabled = true;
        hit = false;
        transform.position = weaponUser.transform.position;
    }

    IEnumerator HitGround()
    {
        mybody.linearVelocity = Vector2.zero;
        myCollider.enabled = false;
        yield return new WaitForSecondsRealtime(1);
        this.gameObject.SetActive(false);
        yield return null;
    }
}
