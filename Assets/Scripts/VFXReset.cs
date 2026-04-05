using System.Collections;
using UnityEngine;

public class VFXReset : MonoBehaviour
{
    ObjectPool objectPool;

    void Awake()
    {
        objectPool = GetComponentInParent<ObjectPool>();
    }

    void OnEnable()
    {
        if(reset != null)
            StopCoroutine(reset);

        reset = StartCoroutine(ResetVFX());
    }

    Coroutine reset;

    IEnumerator ResetVFX()
    {
        yield return new WaitForSecondsRealtime(1);
        objectPool.ReturnObject(this.gameObject);
    }
}
