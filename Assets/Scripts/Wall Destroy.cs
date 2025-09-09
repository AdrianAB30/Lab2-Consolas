using System.Collections;
using UnityEngine;

public class WallDestroy : MonoBehaviour
{
    public GameObject wallNormal;
    public GameObject wallFractured;
    public float explosionForce = 100f;
    public float explosionRadius = 3f;

    public void DestroyWall()
    {
        wallNormal.SetActive(false);
        wallFractured.SetActive(true);

        Rigidbody[] pieces = wallFractured.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        StartCoroutine(DeactivatePiecesOneByOne(pieces, 0.2f)); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            DestroyWall();
        }
    }

    private IEnumerator DeactivatePiecesOneByOne(Rigidbody[] pieces, float delay)
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            MeshRenderer renderer = pieces[i].GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                for (int j = 0; j < 3; j++)
                {
                    renderer.enabled = false;
                    yield return new WaitForSeconds(0.1f);
                    renderer.enabled = true;
                    yield return new WaitForSeconds(0.1f);
                }
            }

            pieces[i].gameObject.SetActive(false);

            yield return new WaitForSeconds(delay);
        }
    }
}
