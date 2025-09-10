using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public static event Action<string> OnBulletReturned;

    [HideInInspector] public string poolTag;

    private bool returned;

    void OnEnable()
    {
        returned = false;
    }

    public void SetInitialVelocity(Vector3 velocity)
    {
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Zombie") || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Destroy"))
    //    {
    //        ReturnToPool();
    //    }
    //}

    private void ReturnToPool()
    {
        if (returned) return;
        returned = true;

        gameObject.SetActive(false);

        OnBulletReturned?.Invoke(poolTag);
    }
}
