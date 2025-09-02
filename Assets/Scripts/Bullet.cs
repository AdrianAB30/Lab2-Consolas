using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public static event Action<string> OnBulletReturned;

    public float lifeTime = 5f;
    [HideInInspector] public string poolTag; 

    private float timer;
    private bool returned; 

    void OnEnable()
    {
        timer = 0f;
        returned = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            ReturnToPool();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        if (returned) return;
        returned = true;

        gameObject.SetActive(false);

        OnBulletReturned?.Invoke(poolTag);
    }
}
