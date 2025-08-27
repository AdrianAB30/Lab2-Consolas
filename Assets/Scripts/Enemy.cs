using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true; 
    }

    private void OnEnable()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);

            Vector3 dir = (target.position - transform.position).normalized;
            dir.y = 0;
            if (dir.sqrMagnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(-dir);
            }
        }
    }

    void Update()
    {
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);

            Vector3 dir = (target.position - transform.position).normalized;
            dir.y = 0;
            if (dir.sqrMagnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(-dir);
            }
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
