using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public float spawnInterval = 10f;
    public int enemiesPerWave = 3;
    public Vector3 spawnAreaSize = new Vector3(50, 0, 50);
    public float spawnHeightOffset = 2f;

    private float timer;

    [SerializeField] private Transform tank;

    void Start()
    {
        SpawnEnemies();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemies();
            timer = 0f;
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-spawnAreaSize.x, spawnAreaSize.x),
                0,
                Random.Range(-spawnAreaSize.z, spawnAreaSize.z)
            );

            Vector3 spawnPos = transform.position + randomPos;
            spawnPos.y = spawnHeightOffset;

            GameObject enemyObj = PoolManager.Instance.SpawnFromPool(enemyTag, spawnPos, Quaternion.identity);

            Enemy enemy = enemyObj.GetComponent<Enemy>();
            enemy.SetTarget(tank);
        }
    }
}
