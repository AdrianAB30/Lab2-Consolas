using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("PowerUps")]
    [SerializeField] private string[] powerUpTags;  
    [SerializeField] private Transform[] spawnPoints;

    [Header("Tiempo de Spawn")]
    [SerializeField] private float minSpawnTime = 10f;
    [SerializeField] private float maxSpawnTime = 20f;

    private GameObject currentPowerUp;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
        PowerUps.OnEffectFinished += HandlePowerUpFinished;
    }

    private void OnDestroy()
    {
        PowerUps.OnEffectFinished -= HandlePowerUpFinished;
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (currentPowerUp == null) 
            {
                float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
                yield return new WaitForSeconds(waitTime);

                SpawnPowerUp();
            }
            yield return null;
        }
    }

    private void SpawnPowerUp()
    {
        if (spawnPoints.Length == 0 || powerUpTags.Length == 0) return;

        int pointIndex = Random.Range(0, spawnPoints.Length);
        int prefabIndex = Random.Range(0, powerUpTags.Length);

        currentPowerUp = PoolManager.Instance.SpawnFromPool(
            powerUpTags[prefabIndex],
            spawnPoints[pointIndex].position,
            Quaternion.identity
        );
    }

    private void HandlePowerUpFinished(PowerUps powerUp)
    {
        if (currentPowerUp != null)
        {
            currentPowerUp.SetActive(false);
            currentPowerUp = null;
        }
    }
}
