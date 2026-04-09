using System;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public static Action onPickUp;
    ObjectPool objectPool;

    void Awake()
    {
        objectPool = GetComponentInParent<ObjectPool>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onPickUp?.Invoke();
            objectPool.ReturnObject(this.gameObject);
        }

    }
}
