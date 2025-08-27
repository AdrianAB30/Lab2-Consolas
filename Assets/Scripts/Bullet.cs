using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f; 
    private float timer;

    void OnEnable()
    {
        timer = 0f; 
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
