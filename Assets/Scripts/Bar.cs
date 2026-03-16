using UnityEngine;
using UnityEngine.InputSystem;

public class Bar : MonoBehaviour
{
    //[field:SerializeField]
    //public int MaxValue {get;private set;}
    //[field:SerializeField]
    //public int Value {get;private set;}
    //public void Change(int amount)
    public int MaxValue;
    public int Value;
    public void Change(int amount)
    {
        Value=Mathf.Clamp(Value+amount,0,MaxValue);

    }
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Change(1);
            Debug.Log("Damage!");
            PlayerController.GetComponent<PlayerController>().ChangeHealth(1);
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Change(-1);
            Debug.Log("Heal!");
            PlayerController.GetComponent<PlayerController>().ChangeHealth(-1);
        }
    }
}
