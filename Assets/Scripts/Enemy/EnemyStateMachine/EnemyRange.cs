using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public bool inRange;
    EnemyLogic enemyLogic;

    void Start()
    {
        enemyLogic = GetComponentInParent<EnemyLogic>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == enemyLogic.targetCircleCollider2d)
        {
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == enemyLogic.targetCircleCollider2d)
        {
            inRange = false;
        }
    }
}
