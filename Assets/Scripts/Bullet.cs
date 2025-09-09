using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public static event Action<string> OnBulletReturned;

    public float lifeTime = 5f;
    [HideInInspector] public string poolTag;

    private float timer;
    private bool returned;
    private Vector3 initialVelocity;
    private float startTime;

    void OnEnable()
    {
        timer = 0f;
        returned = false;
        startTime = Time.time;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (GetComponent<Rigidbody>() == null)
        {
            float elapsedTime = Time.time - startTime;
            Vector3 gravityOffset = 0.5f * Physics.gravity * elapsedTime * elapsedTime;
            transform.position += initialVelocity * Time.deltaTime + gravityOffset;
        }

        if (timer >= lifeTime)
        {
            ReturnToPool();
        }
    }

    public void SetInitialVelocity(Vector3 velocity)
    {
        initialVelocity = velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie") || collision.gameObject.CompareTag("Ground"))
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