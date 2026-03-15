using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int incr;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object that entered the trigger: " + other);
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller !=null )
        {
            controller.ChangeHealth(incr);
            Destroy(gameObject);
        }
    }
}
