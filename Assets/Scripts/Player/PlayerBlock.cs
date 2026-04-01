using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlock : MonoBehaviour
{
    public InputAction blocking_input;

    public Animator anim;

    public CapsuleCollider2D coll;

    void Awake()
    {
        blocking_input.Enable();
    }

    void Start()
    {
        anim=GetComponent<Animator>();
        coll=GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        if (blocking_input.IsPressed())
        {
            anim.SetBool("IsBlocking",true);
            coll.enabled=true;
        }
        else
        {
            anim.SetBool("IsBlocking",false);
            coll.enabled=false;
        }
    } 
}
