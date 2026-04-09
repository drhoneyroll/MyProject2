using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlock : MonoBehaviour
{
    public InputAction blocking_input;

    public Animator anim;

    public CapsuleCollider2D coll;

    public AudioManager audioManager;

    void Awake()
    {
        blocking_input.Enable();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    void Start()
    {
        anim=GetComponent<Animator>();
        coll=GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        if (blocking_input.WasPressedThisFrame())
        {
            audioManager.LoopSFX();
        }
        if (blocking_input.IsPressed())
        {
            anim.SetBool("IsBlocking",true);
            coll.enabled=true;
        }
        else
        {
            audioManager.StopSFX();
            anim.SetBool("IsBlocking",false);
            coll.enabled=false;
        }
    } 
}
