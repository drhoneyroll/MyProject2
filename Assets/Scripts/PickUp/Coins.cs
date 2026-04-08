using System;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public static Action onPickUp;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onPickUp?.Invoke();
            Destroy(gameObject); 
        }

    }
}
