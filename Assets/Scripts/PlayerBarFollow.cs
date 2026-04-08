using UnityEngine;

public class PlayerBarFollow : MonoBehaviour
{
   GameObject followTarget;
   [SerializeField] float yOffset = 0;
   Vector2 offsetPostion;

    void Awake()
    {
        followTarget = FindAnyObjectByType<PlayerController>().gameObject;
    }

    void Update()
    {
        offsetPostion = new Vector2(followTarget.transform.position.x, followTarget.transform.position.y + yOffset);
        transform.position = offsetPostion;
    }
}
