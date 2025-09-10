using UnityEngine;
using System;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public static event Action<string> OnBulletReturned;
    private Collider coll;

    [HideInInspector] public string poolTag;

    private bool returned;

    private void Awake()
    {
        coll = GetComponent<Collider>();
        if (coll == null)
            coll = gameObject.AddComponent<SphereCollider>(); 
    }

    void OnEnable()
    {
        returned = false;
        if (coll != null) coll.isTrigger = true;

        StartCoroutine(Delay(0.3f));
    }
    private void ReturnToPool()
    {
        if (returned) return;
        returned = true;

        gameObject.SetActive(false);

        OnBulletReturned?.Invoke(poolTag);
    }
    private IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        if (coll != null) coll.isTrigger = false;
    }
}
